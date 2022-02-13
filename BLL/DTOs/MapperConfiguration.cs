using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using DAL.Models;

namespace BLL.DTOs
{
    public class MapperConfiguration
    {
        public AutoMapper.MapperConfiguration Configure()
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Project, ProjectDto>().ReverseMap();
                cfg.CreateMap<Task, TaskDto>().ReverseMap();
                cfg.CreateMap<Team, TeamDto>().ReverseMap();
                cfg.CreateMap<User, UserDto>().ReverseMap();
                cfg.CreateMap<UserRegisterDto, User>().ReverseMap();

            });
            return config;
        }
    }
}
