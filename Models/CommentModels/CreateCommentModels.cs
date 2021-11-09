using LapTrinhEZ.Models.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LapTrinhEZ.Models.CommentModels
{
    public class CreateCommentModels
    {
        public int NewsId { get; set; }
        public int CommentId { get; set; }
        [Required]
        public string Remark { get; set; }
        public bool IsReplyComment { get; set; }
    }
}
