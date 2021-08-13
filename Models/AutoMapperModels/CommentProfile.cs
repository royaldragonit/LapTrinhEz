using AutoMapper;
using LapTrinhEZ.Models.CommentModels;
using LapTrinhEZ.Models.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LapTrinhEZ.Models.AutoMapperModels
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<ReplyComment, ListReplyCommentModel>();

            CreateMap<Comment, ListCommentModel>()
                .ForMember(dest => dest.ListReplyComment, opts => opts.MapFrom(src => src.ReplyComment));
            //.ForMember(dest => dest.UserFullName, opts => opts.MapFrom(src => src.User.FullName))
            //.ForMember(dest => dest.ListReplyComment, opts => opts.Ignore());

            //CreateMap<List<Comment>, List<ListCommentModel>>();
        }
    }
}
