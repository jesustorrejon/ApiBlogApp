﻿using ApiBlog.Models;
using ApiBlog.Models.Dtos;
using AutoMapper;

namespace ApiBlog.Mappers
{
    public class BlogMapper : Profile
    {
        public BlogMapper()
        {
            CreateMap<Post, PostDto>().ReverseMap();
            CreateMap<Post, PostCrearDto>().ReverseMap();
            CreateMap<Post, PostActualizarDto>().ReverseMap();
        }
    }
}
