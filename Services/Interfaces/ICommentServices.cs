using LapTrinhEZ.Models.CommentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LapTrinhEZ.Services.Interfaces
{
    public interface ICommentServices
    {
        Task<List<ListCommentModel>> GetListComment(int pageIndex, int pageSize, int newsId);
    }
}
