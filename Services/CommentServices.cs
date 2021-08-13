using AutoMapper;
using LapTrinhEZ.Models.CommentModels;
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
