using LapTrinhEZ.Models.CustomModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LapTrinhEZ.Services.Interfaces
{
    public interface IFileManager
    {
        Task CreateFileWithName(IFormFile formData, string fileName);
        Task<ResultCustomModel<string>> UploadImageNews(IFormFile formData);
    }
}
