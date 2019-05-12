using Microsoft.EntityFrameworkCore;
using MyDiary.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDiary.Data
{
    public interface IApplicationDbContext
    {
        IAsyncEnumerable<Article> ArticlesSet { get; set; }
        IAsyncEnumerable<Comment> CommentsSet { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
        void Delete<T>(T entity) where T : class;
        void Add<T>(T entity) where T : class;
        Task<Article> FindArticleById(int ID);
        Task<Comment> FindCommentById(int ID);
        void Update<T>(T entity) where T : class;
    }
}