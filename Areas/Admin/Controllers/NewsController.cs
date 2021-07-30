using LapTrinhEZ.Areas.Admin.Models.NewsModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LapTrinhEZ.Areas.Admin.Controllers
{
    public class NewsController : AdminBaseController
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}
