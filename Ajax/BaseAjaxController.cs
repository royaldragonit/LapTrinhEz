using LapTrinhEZ.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LapTrinhEZ.Ajax
{
    [EnableCors("Cors")]
    [Route("ajax/[controller]/[action]")]
    public abstract class BaseAjaxController : Controller
    {
        public int _userId = 1;
        public BaseAjaxController()
        {
        }
    }
}
