using LapTrinhEZ.Areas.Admin.Models.NewsModel;
using LapTrinhEZ.Commons;
using LapTrinhEZ.Models.CustomModels;
using LapTrinhEZ.Models.Entites;
using LapTrinhEZ.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LapTrinhEZ.Services
{
    public class NewsServices : BaseServices, INewsServices
    {
        public NewsServices(LaptrinhezdbContext db, IConfiguration config, IWebHostEnvironment environment) : base(db, config, environment)
        {
        }

        /// <summary>
        /// Tạo bài viết
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ResultCustomModel<bool>> CreateNews(CreateNewsInput input)
        {
            var wwwroot = _environment.WebRootPath;
            var data = await _sp.LT_GetUserAsync();
            var lstMenu = await _db.Menu.Where(x => input.Category.Contains(x.MenuId)).ToListAsync();
            if (lstMenu.Count == 0)
            {
                return new ResultCustomModel<bool>
                {
                    Code = 404,
                    Data = false,
                    Message = "Không tồn tại danh mục mà bạn yêu cầu",
                    Success = false
                };
            }
            #region Thêm vào CSDL News
            News n = new News();
            n.Title = input.Title;
            n.Body = input.Content;
            n.CountView = 0;
            n.CreateOn = DateTime.UtcNow;
            n.IsActive = true;
            n.Slug = input.Title.GenerateSlug();
            n.FeatherImage = input.FeatherImage;
            n.UserId = 1;
            foreach (Menu menu in lstMenu)
            {
                NewsMenu nMenu = new NewsMenu();
                nMenu.Menu = menu;
                nMenu.IsActive = true;
                nMenu.CreateOn = DateTime.UtcNow;
                nMenu.News = n;
                n.NewsMenu.Add(nMenu);
            }
            _db.News.Add(n);
            int save = await _db.SaveChangesAsync();
            #endregion
            #region Di chuyển hình ảnh Feather từ Temp qua Final
            string featherImageFileName = Path.GetFileName(input.FeatherImage);
            string fileOld = wwwroot+ CommonConstant.PathUploadTemp+ featherImageFileName;
            string fileNew = wwwroot + CommonConstant.PathUploadFinal + featherImageFileName;
            if (File.Exists(fileOld))
            {
                File.Move(fileOld, fileNew);
            }
            #endregion
            #region Tìm kiếm các hình ảnh trong Content, di chuyển hình từ Temp qua Final
            string[] result = Regex.Matches(input.Content, "src\\s*=\\s*\"(.+?)\"").Cast<Match>().Select(m => m.Value).ToArray();
            foreach (string img in result)
            {
                string imgFile= Path.GetFileName(img).Replace("\"","");
                 fileOld = wwwroot + CommonConstant.PathUploadTemp + imgFile;
                 fileNew = wwwroot + CommonConstant.PathUploadFinal + imgFile;
                if (File.Exists(fileOld))
                {
                    //Di chuyển qua Final
                    File.Move(fileOld, fileNew);
                }
            }
            //Thay thế Url Imgae trong đó
            input.Content.Replace(CommonConstant.PathUploadTemp, CommonConstant.PathUploadFinal);
            #endregion
            return new ResultCustomModel<bool>
            {
                Code = 200,
                Success = 1 > 0,
                Data = true,
                Message = "Thêm thành công với số dòng được thêm là: " + 1
            };
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
