using AutoMapper;
using BusinessServiceLayer.DTO;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServiceLayer
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Student, StudentDTO>()
                .ForMember(dest => dest.GradeValue, opt => opt.MapFrom(src => src.Grade.GradeValue))
                .ForMember(dest => dest.AddressDTO, opt => opt.MapFrom(src => src.Address));
            ;
            CreateMap<StudentDTO, Student>();
            CreateMap<AddressDTO, StudentAddress>();
            CreateMap<StudentAddress, AddressDTO>();
            CreateMap<StudentInputDTO, Student>();
            CreateMap<Grade, GradeOutputDTO>().ReverseMap();
            CreateMap<Grade, GradeInputDTO>().ReverseMap();
            CreateMap<CourseDTO, Course>().ReverseMap();
            CreateMap<StudentInCourse, StudentInCourseDTO>().ReverseMap();
            CreateMap<StudentInCourseInputDTO, StudentInCourse>().ReverseMap();
            CreateMap<Course, InputCourseDTO>().ReverseMap();
        }
    }
}
