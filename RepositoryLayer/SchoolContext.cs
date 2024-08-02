using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer
{
  public class SchoolContext : DbContext
  {
    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
    {
      Database.EnsureCreated();
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Grade> Grades { get; set; }
  }
}
