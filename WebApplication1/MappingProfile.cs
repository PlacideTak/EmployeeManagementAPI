using AutoMapper;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserReadDTO>();
            CreateMap<UserCreateDto, User>();

            // Employee
            CreateMap<Employee, EmployeeReadDTO>();

            CreateMap<EmployeeCreateDTO, Employee>();

            // Project
            CreateMap<Project, ProjectReadDTO>();
            CreateMap<ProjectCreateUpdateDTO, Project>();

            CreateMap<ProjectCreateUpdateDTO, Project>()
            .ForMember(dest => dest.Employees, opt => opt.Ignore()) // ignore la collection
            .AfterMap((src, dest, ctx) => {
                if (src.EmployeeIds != null)
                {
                    dest.Employees = [.. src.EmployeeIds.Select(id => new Employee { Id = id })];
                }
            });

            CreateMap<Employee, EmployeeReadDTO>()
            .ForMember(dest => dest.Projects,
                opt => opt.MapFrom(src => src.Projects));

                }

    }
}
