﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace LapTrinhEZ.Models.Entites
{
    public partial class Menu
    {
        public Menu()
        {
            NewsMenu = new HashSet<NewsMenu>();
            SubMenu = new HashSet<SubMenu>();
        }

        public int MenuId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public DateTime CreateOn { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<NewsMenu> NewsMenu { get; set; }
        public virtual ICollection<SubMenu> SubMenu { get; set; }
    }
}