using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LapTrinhEZ.Areas.Admin.Models.NewsModel
{
    public class UpLoadFile
    {
        public IFormFile FileUpload { get; set; }
    }
}
