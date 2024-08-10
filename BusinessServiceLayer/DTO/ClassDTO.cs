using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessServiceLayer.DTO
{
    public class ClassDTO
    {
        public Class Classes { set; get; }
        public ClassDTO() { }
    }
}