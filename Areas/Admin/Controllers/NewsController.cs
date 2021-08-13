using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LapTrinhEZ.Models.Entites;
using LapTrinhEZ.Services.Interfaces;

namespace LapTrinhEZ.Areas.Admin.Controllers
{
    public class NewsController : AdminBaseController
    {
        private readonly LaptrinhezdbContext _db;
        private readonly INewsServices _newsServices;

        public NewsController(LaptrinhezdbContext db, INewsServices newsServices)
        {
            _db = db;
            _newsServices = newsServices;
        }

        // GET: Admin/News
        public async Task<IActionResult> Index()
        {
            var laptrinhezdbContext = _db.News.Include(n => n.User);
            return View(await laptrinhezdbContext.ToListAsync());
        }

        // GET: Admin/News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _db.News
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.NewsId == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: Admin/News/Create
        public IActionResult Create()
        {
            ViewBag.ListCategory = _newsServices.ListCategory().Result;
            return View();
        }
        public IActionResult Edit(int id,string slug)
        {
            var news = _newsServices.GetNews(id).Result;
            ViewBag.ListCategory= _newsServices.ListCategory().Result;
            return View(news);
        }

        // GET: Admin/News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _db.News
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.NewsId == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var news = await _db.News.FindAsync(id);
            _db.News.Remove(news);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
            return _db.News.Any(e => e.NewsId == id);
        }
    }
}
