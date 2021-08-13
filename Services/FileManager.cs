using AutoMapper;
using LapTrinhEZ.Commons;
using LapTrinhEZ.Models.CustomModels;
using LapTrinhEZ.Models.Entites;
using LapTrinhEZ.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LapTrinhEZ.Services
{
    public class FileManager :BaseServices, IFileManager
    {
        public FileManager(LaptrinhezdbContext db, IConfiguration config, IWebHostEnvironment environment, IMapper mapper) : base(db, config, environment, mapper)
        {
        }

        public async Task CreateFileWithName(IFormFile formData, string fileName)
        {
            string fullPath = @"E:\Project\LapTrinhEZ\LapTrinhEZ\wwwroot\imgUpload\temp";
            await using FileStream output = File.Create(Path.Combine(fullPath, fileName));
            await formData.CopyToAsync(output);
        }

        public async Task<ResultCustomModel<string>> UploadImageNews(IFormFile formData)
        {
            string fullPath = @"E:\Project\LapTrinhEZ\LapTrinhEZ\wwwroot\imgUpload\temp";
            await using FileStream output = File.Create(Path.Combine(fullPath, formData.FileName));
            await formData.CopyToAsync(output);
            return new ResultCustomModel<string>
            {
                Success = true,
                Message = "Upload thành công",
                Code = 200,
                Data = _host + CommonConstant.PathUploadTemp + formData.FileName
            };
        }
    }
}
