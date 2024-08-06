using AutoMapper;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServiceLayer
{
    public class MapperConfig :  Profile
    {
        public MapperConfig()
        {
            CreateMap<Student, StudentDTO>().ForMember(dest => dest.GradeValue, opt => opt.MapFrom(src => src.Grade.GradeValue));
            CreateMap<StudentDTO, Student>();
        }
    }
}
