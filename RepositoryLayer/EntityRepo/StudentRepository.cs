
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;

namespace RepositoryLayer.EntityRepo
{
    public class StudentRepository : BaseRepository<Student>
    {
        public StudentRepository(SchoolContext dbContext) : base(dbContext)
        {
        }
        //public override async Task<Student> Get(int id)
        //{
        //    var student = await dbContext.Students.Include(c => c.Grade).FirstOrDefaultAsync(c => c.Id == id);
        //    //return base.Get(id);
        //    return student;
        //}
    }
}
