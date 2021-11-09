using AutoMapper;
using LapTrinhEZ.Models.CommentModels;
using LapTrinhEZ.Models.CustomModels;
using LapTrinhEZ.Models.Entites;
using LapTrinhEZ.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LapTrinhEZ.Services
{
    public class CommentServices : BaseServices, ICommentServices
    {
        public CommentServices(LaptrinhezdbContext db, IConfiguration config, IWebHostEnvironment environment, IMapper mapper) : base(db, config, environment, mapper)
        {
        }

        public async Task<ResultCustomModel<bool>> CreateComment(CreateCommentModels input, int _userId)
        {
            int save = 0;
            if (input.IsReplyComment)
            {
                ReplyComment reply = new ReplyComment();
                reply.CommentId = input.CommentId;
                reply.CreateOn = DateTime.Now;
                reply.IsActive = true;
                reply.Remark = input.Remark;
                reply.UserId = _userId;
                _db.Entry(reply).State = EntityState.Added;
            }
            else
            {
                Comment comment = new Comment();
                comment.CommentId = input.CommentId;
                comment.CreateOn = DateTime.Now;
                comment.IsActive = true;
                comment.Remark = input.Remark;
                comment.UserId = _userId;
                _db.Entry(comment).State = EntityState.Added;
            }
            save += await _db.SaveChangesAsync();
            return new ResultCustomModel<bool>
            {
                Code = 200,
                Data = save > 0,
                Message = save > 0?"Gửi bình luận thành công": "Gửi bình luận không thành công",
                Success = save > 0
            };
        }

        public async Task<List<ListCommentModel>> GetListComment(int pageIndex, int pageSize, int newsId)
        {
            if (pageIndex < 1)
                pageIndex = 1;
            if (pageSize < 10)
                pageSize = 10;
            List<Comment> lstComments = await _db.Comment
                .Include(c => c.ReplyComment).ThenInclude(r => r.User) //cai nay la user cua replycomment
                .Include(c => c.User)// cai nay la use cua comment
                .Where(c => c.NewsId == newsId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            List<ListCommentModel> lstData = _mapper.Map<List<ListCommentModel>>(lstComments);
            return lstData;
        }
    }
}
