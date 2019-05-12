using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyDiary.Data;
using MyDiary.Models;
using MyDiary.VIewModel;

namespace MyDiary.Controllers
{
    public class CommentsController : Controller
    {
        private IApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        private CommentsController() { }
        public static CommentsController Of(IApplicationDbContext applicationDbContext)
        {
            CommentsController commentsController = new CommentsController();
            commentsController._context = applicationDbContext;
            return commentsController;
        }

        // GET: Comments
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View("Index", await _context.CommentsSet.ToList());
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }

            var comment = await _context.FindCommentById((int)id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comments/Create
        [Authorize]
        public IActionResult Create(int id)
        {
            ArticleCommentViewModel model = new ArticleCommentViewModel();
            model.id = id;
            return View(model);
        }

        // POST: Comments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content")] Comment comment, int id)
        {
            var thisUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if ((await _context.FindArticleById(id)) != null)
            {
                if (ModelState.IsValid)
                {
                    comment.Article_id = id;
                    comment.Author_id = thisUserId;
                    _context.Add(comment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Articles", new { id = id});
                }
                return View(comment);
            }
            return NotFound();
        }

        // GET: Comments/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.FindCommentById((int)id);
            if (comment == null)
            {
                return NotFound();
            }
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,Article_id,Author_id")] Comment comment)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Articles", new { id = comment.Article_id });
            }
            return View(comment);
        }

        // GET: Comments/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            { 
                return NotFound();
            }

            var comment = await _context.FindCommentById((int)id);
            if (comment == null)
            {
                throw new ArgumentException("Parameter out of bounds");
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _context.FindCommentById(id);
            int savedArticleId = comment.Article_id;
            _context.Delete(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Articles", new { id = savedArticleId });
        }

        private bool CommentExists(int id)
        {
            return _context.CommentsSet.ToEnumerable().Any(e => e.Id == id);
        }
    }
}
