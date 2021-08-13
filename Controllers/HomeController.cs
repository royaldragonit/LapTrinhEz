using LapTrinhEZ.Models.Entites;
using LapTrinhEZ.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LapTrinhEZ.Controllers
{
    public class HomeController : Controller
    {
        private readonly INewsServices _newsServices;
        public HomeController(INewsServices newsServices)
        {
            _newsServices = newsServices;
        }
        public IActionResult Index()
        {
            List<LT_GetNewsFeatherResult> newsFeather = _newsServices.GetNewsFeather().Result;
            ViewBag.NewsRelate= _newsServices.GetNewsRelate().Result;
            ViewBag.NewsMMO= _newsServices.GetNewsMMO().Result;
            ViewBag.NewsViews= _newsServices.GetNewsViews().Result;
            ViewBag.HostName = Request.Scheme + "://" + Request.Host;
            return View(newsFeather);
        }
    }
}
