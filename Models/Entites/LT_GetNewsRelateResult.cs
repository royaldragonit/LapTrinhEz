﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LapTrinhEZ.Models.Entites
{
    public partial class LT_GetNewsRelateResult
    {
        public int NewsId { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int CountView { get; set; }
        public string Description { get; set; }
        public DateTime CreateOn { get; set; }
        public int UserId { get; set; }
        public string FeatherImage { get; set; }
        public string Category { get; set; }
        public string CategorySlug { get; set; }
        public string Author { get; set; }
        public int? DisLike { get; set; }
        public int? Like { get; set; }
        public int Views { get; set; }
        public bool IsFeather { get; set; }
        public int MenuId { get; set; }
    }
}
