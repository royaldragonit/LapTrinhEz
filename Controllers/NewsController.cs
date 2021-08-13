using LapTrinhEZ.Models.Entites;
using LapTrinhEZ.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LapTrinhEZ.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsServices _newsServices;
        private readonly ICommentServices _commentServices;
        public NewsController(INewsServices newsServices, ICommentServices commentServices)
        {
            _newsServices = newsServices;
            _commentServices = commentServices;
        }
        [Route("News/{id?}/{slug?}")]
        public IActionResult Index(int id, string slug)
        {
            if (id == 0 || string.IsNullOrEmpty(slug))
                return NotFound();
            ViewBag.HostName = Request.Scheme + "://" + Request.Host;
            VNews data = _newsServices.GetNews(id).Result;
           var listComments= _commentServices.GetListComment(pageIndex: 1, pageSize: 10, newsId: id).Result;
            ViewBag.ListComment = listComments;
            return View(data);
        }
    }
}
