﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace LapTrinhEZ.Models.Entites
{
    public partial class Comment
    {
        public Comment()
        {
            LikeComent = new HashSet<LikeComent>();
            ReplyComment = new HashSet<ReplyComment>();
        }

        public int CommentId { get; set; }
        public int NewsId { get; set; }
        public int UserId { get; set; }
        public string Remark { get; set; }
        public DateTime CreateOn { get; set; }
        public bool? IsActive { get; set; }

        public virtual News News { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<LikeComent> LikeComent { get; set; }
        public virtual ICollection<ReplyComment> ReplyComment { get; set; }
    }
}