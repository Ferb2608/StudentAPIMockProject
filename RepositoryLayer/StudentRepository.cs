namespace RepositoryLayer
{
  public class StudentRepository : BaseRepository<Student>
  {
    public StudentRepository(SchoolContext dbContext) : base(dbContext)
    {
    }
  }
}
