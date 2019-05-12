using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyDiary.Data;
using MyDiary.Models;
using MyDiary.VIewModel;

namespace MyDiary.Controllers
{
    public class ArticlesController : Controller
    {
        private IApplicationDbContext _context;

        public ArticlesController(ApplicationDbContext context)
        {
            _context = context;
        }
        private ArticlesController() { }
        public static ArticlesController Of(IApplicationDbContext applicationDbContext)
        {
            ArticlesController articlesController = new ArticlesController();
            articlesController._context = applicationDbContext;
            return articlesController;
        }

        // GET: Articles
        public async Task<IActionResult> Index()
        {
            return View(await _context.ArticlesSet.ToList());
        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ArticleAndCommentsViewModel model = new ArticleAndCommentsViewModel();
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.FindArticleById((int)id);
            if (article == null)
            {
                return RedirectToAction("Index", "Articles");
            }
            model.Article = article;

            var quey = from comment 
                       in (await _context.CommentsSet.ToList())
                       where (comment.Article_id == id)
                       select comment;

            List<Comment> ListOfComments = new List<Comment>();
            
            foreach (var item in quey)
            {
                ListOfComments.Add(item);
            }
            model.Comments = ListOfComments;
            return View(model);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content,Title")] Article article)
        {
            if (ModelState.IsValid)
            {
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        // GET: Articles/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.FindArticleById((int)id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,Title")] Article article)
        {
            if (id != article.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        // GET: Articles/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.FindArticleById((int)id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.FindArticleById((int)id); ;
            _context.Delete(article);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
            return _context.ArticlesSet.ToEnumerable().Any(e => e.Id == id);
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> VerifyTitle(string title)
        {
            var article = await _context.ArticlesSet
                .FirstOrDefault(m => m.Title.Equals(title));
            if(char.IsLower(title[0]))
            {
                return Json($"Title {title} first letter should be upper");
            }
            if (article == null)
                return Json(true);
            else
                return Json($"Title {title} is already in use");
        }
    }
}
