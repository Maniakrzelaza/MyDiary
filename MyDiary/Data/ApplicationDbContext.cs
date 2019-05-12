using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyDiary.Models;

namespace MyDiary.Data
{
    public class ApplicationDbContext : IdentityDbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Article> Articles { get; set; }
        IAsyncEnumerable<Comment> IApplicationDbContext.CommentsSet
        {
            get { return Comments.ToAsyncEnumerable(); }
            set { }
        }

        IAsyncEnumerable<Article> IApplicationDbContext.ArticlesSet
        {
            get { return Articles.ToAsyncEnumerable(); }
            set { }
        }
        public void Delete<T>(T entity) where T : class
        {
            Set<T>().Remove(entity);
        }

        public Task<Article> FindArticleById(int ID)
        {
            return Set<Article>().FindAsync(ID);
        }

        public Task<Comment> FindCommentById(int ID)
        {
            return Set<Comment>().FindAsync(ID);
        }

        Task<int> IApplicationDbContext.SaveChangesAsync()
        {
            Task<int> result = SaveChangesAsync();
            return result; 
        }

        void IApplicationDbContext.Add<T>(T entity)
        {
            Set<T>().Add(entity);
        }

        int IApplicationDbContext.SaveChanges()
        {
            return SaveChanges();
        }

        void IApplicationDbContext.Update<T>(T entity)
        {
            Set<T>().Update(entity);
        }
    }
}
