using LapTrinhEZ.Areas.Admin.Models.NewsModel;
using LapTrinhEZ.Models.CustomModels;
using LapTrinhEZ.Models.Entites;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LapTrinhEZ.Services.Interfaces
{
    public interface INewsServices
    {
        Task<ResultCustomModel<bool>> CreateNews(CreateNewsInput input);
        Task<ResultCustomModel<string>> UploadImageNews(IFormFile formData);
        Task<List<LT_GetNewsFeatherResult>> GetNewsFeather();
        Task<List<LT_GetNewsRelateResult>> GetNewsRelate();
        Task<List<LT_GetNewsMMOResult>> GetNewsMMO();
        Task<List<LT_GetNewsViewsResult>> GetNewsViews();
        Task<VNews> GetNews(int newsId);
        Task<List<Menu>> ListCategory();
        Task<ResultCustomModel<bool>> EditNews(CreateNewsInput input);
    }
}
