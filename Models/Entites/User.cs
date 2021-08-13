﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace LapTrinhEZ.Models.Entites
{
    public partial class User
    {
        public User()
        {
            Comment = new HashSet<Comment>();
            LikeComent = new HashSet<LikeComent>();
            News = new HashSet<News>();
            NewsCategory = new HashSet<NewsCategory>();
            NewsComment = new HashSet<NewsComment>();
            ReplyComment = new HashSet<ReplyComment>();
            UserRole = new HashSet<UserRole>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? Birthday { get; set; }
        public bool? Gender { get; set; }
        public DateTime CreateOn { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<LikeComent> LikeComent { get; set; }
        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<NewsCategory> NewsCategory { get; set; }
        public virtual ICollection<NewsComment> NewsComment { get; set; }
        public virtual ICollection<ReplyComment> ReplyComment { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}