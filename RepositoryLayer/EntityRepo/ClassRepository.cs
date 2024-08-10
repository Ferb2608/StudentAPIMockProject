using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.EntityRepo
{
    public class ClassRepository : BaseRepository<Class>
    {
        public ClassRepository(SchoolContext dbContext) : base(dbContext)
        {
        }
    }
}
