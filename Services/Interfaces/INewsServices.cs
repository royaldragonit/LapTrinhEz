using LapTrinhEZ.Areas.Admin.Models.NewsModel;
using LapTrinhEZ.Models.CustomModels;
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
    }
}
