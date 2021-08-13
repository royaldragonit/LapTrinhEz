using LapTrinhEZ.Models.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LapTrinhEZ.Models.CommentModels
{
    public class ListCommentModel
    {
        public int CommentId { get; set; }
        public string Remark { get; set; }
        public DateTime CreateOn { get; set; }
        public int UserId { get; set; }
        public string UserFullName { get; set; } //cai nay no ngam hieu la User.FullName do
        public List<ListReplyCommentModel> ListReplyComment { get; set; }
    }
}
