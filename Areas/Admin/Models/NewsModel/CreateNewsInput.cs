using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LapTrinhEZ.Areas.Admin.Models.NewsModel
{
    public class CreateNewsInput
    {
        public int NewsId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public string FeatherImage { get; set; }
        public string Slug { get; set; }
        [Required]
        public int[] Category { get; set; }
    }
}
