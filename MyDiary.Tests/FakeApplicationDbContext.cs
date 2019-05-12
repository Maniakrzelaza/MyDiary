using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Data;
using MyDiary.Models;
namespace MyDiaryTests
{
    class FakeApplicationDbContext : IApplicationDbContext
    {
        SetMap _map = new SetMap();
        public IAsyncEnumerable<Article> ArticlesSet
        {
            get { return _map.Get<Article>().ToAsyncEnumerable(); }
            set { _map.Use<Article>(value.ToEnumerable()); }
        }

        public IAsyncEnumerable<Comment> CommentsSet
        {
            get { return _map.Get<Comment>().ToAsyncEnumerable(); }
            set { _map.Use<Comment>(value.ToEnumerable()); }
        }

        public bool ChangesSaved { get; set; }
        Task<int> IApplicationDbContext.SaveChangesAsync()
        {
            return Task.FromResult<int>(5);
        }

        public int SaveChanges()
        {
            ChangesSaved = true;
            return 0;
        }

        public void Add<T>(T entity) where T : class
        {
            _map.Get<T>().Add(entity);
        }

        public Task<Article> FindArticleById(int ID)
        {
            Article item = (from p in this.ArticlesSet.ToEnumerable()
                          where p.Id == ID
                          select p).First();
            
           return Task.FromResult<Article>(item);
        }

        public Task<Comment> FindCommentById(int ID)
        {
            Comment item = (from c in this.CommentsSet.ToEnumerable()
                            where c.Id == ID
                            select c).First();
            return Task.FromResult<Comment>(item);
        }


        public void Delete<T>(T entity) where T : class
        {
            _map.Get<T>().Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            var entityToDelete = _map.Get<T>().First(e => e == entity);
            _map.Get<T>().Remove(entityToDelete);
            _map.Get<T>().Add(entity);
        }

        class SetMap : KeyedCollection<Type, object>
        {

            public HashSet<T> Use<T>(IEnumerable<T> sourceData)
            {
                var set = new HashSet<T>(sourceData);
                if (Contains(typeof(T)))
                {
                    Remove(typeof(T));
                }
                Add(set);
                return set;
            }

            public HashSet<T> Get<T>()
            {
                return (HashSet<T>)this[typeof(T)];
            }

            protected override Type GetKeyForItem(object item)
            {
                return item.GetType().GetGenericArguments().Single();
            }
        }
    }
}

