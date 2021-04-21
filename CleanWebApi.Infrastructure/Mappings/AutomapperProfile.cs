using AutoMapper;
using CleanWebApi.Core.DTOs;
using CleanWebApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanWebApi.Infrastructure.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            //Instalar el Automapper con inyeccion de dependencia, ayuda a evitar la referencia circular
            CreateMap<Post, PostDTO>(); // Mapeo de Post a PostDTO para los GET
            CreateMap<PostDTO, Post>(); // Mapeo de PostDTO a Post para los POST, PUT, DELETE
        }
    }
}
