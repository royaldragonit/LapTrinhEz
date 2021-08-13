using AutoMapper;
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
        public NewsServices(LaptrinhezdbContext db, IConfiguration config, IWebHostEnvironment environment, IMapper mapper) : base(db, config, environment, mapper)
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
            n.CountView = 0;
            n.CreateOn = DateTime.UtcNow;
            n.IsActive = true;
            n.Slug = input.Title.GenerateSlug();
            n.FeatherImage = input.FeatherImage;
            n.UserId = 1;
            #endregion
            #region Di chuyển hình ảnh Feather từ Temp qua Final
            string featherImageFileName = Path.GetFileName(input.FeatherImage);
            string fileOld = wwwroot + CommonConstant.PathUploadTemp + featherImageFileName;
            string fileNew = wwwroot + CommonConstant.PathUploadFinal + featherImageFileName;
            if (File.Exists(fileOld))
            {
                File.Move(fileOld, fileNew);
            }
            #endregion
            #region Tìm kiếm các hình ảnh trong Content, di chuyển hình từ Temp qua Final
            string[] result = Regex.Matches(input.Content, "src\\s*=\\s*\"(.+?)\"").Cast<Match>().Select(m => m.Value).ToArray();
            input.Content.Replace(wwwroot + CommonConstant.PathUploadTemp, wwwroot + CommonConstant.PathUploadFinal);
            //Xoá tất cả các thẻ Script đề phòng XSS
            Regex.Replace(input.Content, @"<script[^>]*>[\s\S]*?</script>", "");
            foreach (string img in result)
            {
                string imgFile = Path.GetFileName(img).Replace("\"", "");
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
            n.Body = input.Content;
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
            #endregion
            int save = await _db.SaveChangesAsync();
            return new ResultCustomModel<bool>
            {
                Code = 200,
                Success = save > 0,
                Data = true,
                Message = "Thêm thành công với số dòng được thêm là: " + save
            };
        }
        /// <summary>
        /// Sửa bài viết
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ResultCustomModel<bool>> EditNews(CreateNewsInput input)
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
            #region Sửa News
            News n = await _db.News.FirstOrDefaultAsync(x => x.NewsId == input.NewsId);
            if (n == null)
            {
                return new ResultCustomModel<bool>
                {
                    Code = 404,
                    Data = false,
                    Message = "Không tìm thấy bài viết",
                    Success = false
                };
            }
            n.Title = input.Title;
            n.Slug = input.Title.GenerateSlug();
            n.FeatherImage = input.FeatherImage;
            n.UserId = 1;
            //Xoá những tham chiếu
            List<NewsMenu> lstNewsMenu = await _db.NewsMenu.Where(x => x.NewsId == input.NewsId).ToListAsync();
            foreach (NewsMenu item in lstNewsMenu)
            {
                _db.Entry(item).State = EntityState.Deleted;
            }
            int save = await _db.SaveChangesAsync();
            //Thêm lại
            foreach (Menu menu in lstMenu)
            {
                NewsMenu nMenu = new NewsMenu();
                nMenu.Menu = menu;
                nMenu.IsActive = true;
                nMenu.CreateOn = DateTime.UtcNow;
                nMenu.News = n;
                n.NewsMenu.Add(nMenu);
            }
            _db.Entry(n).State=EntityState.Modified;
            #endregion
            string fileOld = "", fileNew = "";
            #region Di chuyển hình ảnh Feather từ Temp qua Final
            if (!string.IsNullOrEmpty(input.FeatherImage))
            {
                string featherImageFileName = Path.GetFileName(input.FeatherImage);
                fileOld = wwwroot + CommonConstant.PathUploadTemp + featherImageFileName;
                fileNew = wwwroot + CommonConstant.PathUploadFinal + featherImageFileName;
                if (File.Exists(fileOld))
                {
                    File.Move(fileOld, fileNew);
                }
            }
            #endregion
            #region Tìm kiếm các hình ảnh trong Content, di chuyển hình từ Temp qua Final
            string[] result = Regex.Matches(input.Content, "src\\s*=\\s*\"(.+?)\"").Cast<Match>().Select(m => m.Value).ToArray();
            input.Content.Replace(wwwroot + CommonConstant.PathUploadTemp, wwwroot + CommonConstant.PathUploadFinal);
            //Xoá tất cả các thẻ Script đề phòng XSS
            Regex.Replace(input.Content, @"<script[^>]*>[\s\S]*?</script>", "");
            foreach (string img in result)
            {
                string imgFile = Path.GetFileName(img).Replace("\"", "");
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
            n.Body = input.Content;
            #endregion
            save += await _db.SaveChangesAsync();
            return new ResultCustomModel<bool>
            {
                Code = 200,
                Success = save > 0,
                Data = true,
                Message = "Thêm thành công với số dòng được thêm là: " + save
            };
        }

        public async Task<VNews> GetNews(int newsId)
        {
            var data = await _db.VNews.FirstOrDefaultAsync(x => x.NewsId == newsId);
            return data;
        }

        public async Task<List<LT_GetNewsFeatherResult>> GetNewsFeather()
        {
            var data = await _sp.LT_GetNewsFeatherAsync();
            return data;
        }

        public async Task<List<LT_GetNewsMMOResult>> GetNewsMMO()
        {
            var data = await _sp.LT_GetNewsMMOAsync();
            return data;
        }

        public async Task<List<LT_GetNewsRelateResult>> GetNewsRelate()
        {
            var data = await _sp.LT_GetNewsRelateAsync();
            return data;
        }

        public async Task<List<LT_GetNewsViewsResult>> GetNewsViews()
        {
            var data = await _sp.LT_GetNewsViewsAsync();
            return data;
        }

        public async Task<List<Menu>> ListCategory()
        {
            List<Menu> lstMenu = await _db.Menu.ToListAsync();
            return lstMenu;
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
