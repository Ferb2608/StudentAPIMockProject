using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServiceLayer
{
    public class MapperConfig<S, D> : Profile
    {
        public MapperConfig()
        {
            CreateMap<S, D>().ReverseMap();
        }
    }
}
