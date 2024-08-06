using AutoMapper;
using RepositoryLayer;

namespace BusinessServiceLayer
{
  public class StudentService
  {
        StudentRepository studentRepository;
        private readonly IMapper mapper;
        public StudentService(StudentRepository studentRepository, IMapper mapper)
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
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

        public async Task<StudentDTO> Get(int id)
        {
            var student = await studentRepository.Get(id: id, s => s.Grade);
            //return new StudentDTO(student.Id, student.FirstName, student.LastName, student.Phone);
            var studentDto = mapper.Map<StudentDTO>(student);
            return studentDto;
        }
    }
}
