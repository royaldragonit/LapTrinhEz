using LapTrinhEZ.Models.CommentModels;
using LapTrinhEZ.Models.CustomModels;
using LapTrinhEZ.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LapTrinhEZ.Ajax
{
    public class CommentController : BaseAjaxController
    {
        private readonly ICommentServices _commentServices;
        public CommentController(ICommentServices commentServices)
        {
            _commentServices = commentServices;
        }

        [HttpPost]
        public ResultCustomModel<bool> CreateComment([FromBody] CreateCommentModels input)
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
            var data = _commentServices.CreateComment(input,_userId).Result;
            return data;
        }
    }
}
