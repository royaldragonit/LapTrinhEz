using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LapTrinhEZ.Models.CommentModels
{
    public class ListReplyCommentModel
    {
        public int ReplyCommentId { get; set; }
        public string Remark { get; set; }
        public DateTime CreateOn { get; set; }
        public int UserId { get; set; }
        public string UserFullName { get; set; }
    }
}
