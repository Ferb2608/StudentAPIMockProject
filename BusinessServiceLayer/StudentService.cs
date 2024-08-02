using RepositoryLayer;

namespace BusinessServiceLayer
{
  public class StudentService : BaseService<Student, StudentDTO>
  {
        public StudentService(StudentRepository studentRepository) : base(studentRepository)
        {

        }

        //public StudentService(StudentRepository studentRepository)
        //{
        //}

        //public IEnumerable<StudentDTO> Get(int pageNumber = 1, int pageSize = 10)
        //{
        //    var dtos = new List<StudentDTO>();
        //    var entities = studentRepository.Get(pageNumber: pageNumber, pageSize: pageSize,
        //                                        orderBy: q => q.OrderByDescending(s => s.Phone),
        //                                        filter: q => q.LastName.ToLower().Contains("t"));

        //    foreach (var entity in entities)
        //        dtos.Add(new StudentDTO(entity.Id, entity.FirstName, entity.LastName, entity.Phone));

        //    return dtos;
        //}

        //public async Task<StudentDTO> Get(int id)
        //{
        //  var student = await studentRepository.Get(id: id);
        //  return new StudentDTO(student.Id, student.FirstName, student.LastName, student.Phone);
        //}
    }
}
