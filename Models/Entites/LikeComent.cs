﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace LapTrinhEZ.Models.Entites
{
    public partial class LikeComent
    {
        public int LikeComentId { get; set; }
        public int UserId { get; set; }
        public DateTime CreateOn { get; set; }
        public bool? IsActive { get; set; }
        public int CommentId { get; set; }

        public virtual Comment Comment { get; set; }
        public virtual User User { get; set; }
    }
}