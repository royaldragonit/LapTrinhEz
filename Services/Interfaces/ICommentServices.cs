using LapTrinhEZ.Models.CommentModels;
using LapTrinhEZ.Models.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LapTrinhEZ.Services.Interfaces
{
    public interface ICommentServices
    {
        Task<List<ListCommentModel>> GetListComment(int pageIndex, int pageSize, int newsId);
        Task<ResultCustomModel<bool>> CreateComment(CreateCommentModels input, int _userId);
    }
}
