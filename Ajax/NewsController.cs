using LapTrinhEZ.Areas.Admin.Models.NewsModel;
using LapTrinhEZ.Models.CustomModels;
using LapTrinhEZ.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LapTrinhEZ.Ajax
{
    public class NewsController : BaseAjaxController
    {
        public NewsController(INewsServices newsServices) : base(newsServices)
        {
        }
        [HttpPost]
        public ResultCustomModel<bool> CreateNews([FromBody]CreateNewsInput input)
        {
            if (!ModelState.IsValid)
            {
                return new ResultCustomModel<bool>
                {
                    Code = 400,
                    Data = false,
                    Message = "Dữ liệu gửi lên bị thiếu hoặc không đúng định dạng, vui lòng thử lại",
                    Success = false
                };
            }
            var data = _newsServices.CreateNews(input).Result;
            return data;
        }
        [HttpPost]
        public ResultCustomModel<string> UploadImageNews()
        {
            if (HttpContext.Request.Form.Files.Count == 0)
                return new ResultCustomModel<string>
                {
                    Code = 404,
                    Data = "Không tìm thấy file mà bạn gửi",
                    Message = null,
                    Success = false
                };
            var data = _newsServices.UploadImageNews(HttpContext.Request.Form.Files[0]).Result;
            return data;
        }
    }
}
