using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Entity;

namespace RepositoryLayer.EntityRepo
{
    public class GradeRepository : BaseRepository<Grade>
    {
        public GradeRepository(SchoolContext dbContext) : base(dbContext)
        {
        }
    }
}
