using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.EntityRepo
{
    public class CourseRepository : BaseRepository<Course>
    {
        public CourseRepository(SchoolContext dbContext) : base(dbContext)
        {
        }
    }
}
