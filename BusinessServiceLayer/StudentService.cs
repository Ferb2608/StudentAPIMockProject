using AutoMapper;
using RepositoryLayer;

namespace BusinessServiceLayer
{
  public class StudentService
  {
        private readonly StudentRepository studentRepository;
        private readonly GradeRepository gradeRepository;
        private readonly IMapper mapper;
        public StudentService(StudentRepository studentRepository, IMapper mapper, GradeRepository gradeRepository)
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
            this.gradeRepository = gradeRepository;
        }

        public async Task<IEnumerable<StudentDTO>> Get(int pageNumber = 1, int pageSize = 10)
        {
            var dtos = new List<StudentDTO>();
            var entities = await studentRepository.Get(pageNumber: pageNumber, pageSize: pageSize,
                                                orderBy: q => q.OrderByDescending(s => s.Id),
                                                includes: s => s.Grade);
            foreach (var entity in entities) 
            {
                var studentDto = mapper.Map<StudentDTO>(entity);
                dtos.Add(studentDto);
            }

            return dtos;
        }

        public async Task<StudentDTO> Get(int id)
        {
            var student = await studentRepository.Get(id: id, s => s.Grade);
            var studentDto = mapper.Map<StudentDTO>(student);
            return studentDto;
        }
        public async Task<StudentDTO> Post(StudentDTO studentDTO)
        {
            var student = mapper.Map<Student>(studentDTO);
            var grades = await gradeRepository.GetByProperty(t => t.GradeValue == studentDTO.GradeValue);
            var grade = grades.FirstOrDefault();
            if (grade != null)
            {
                student.Grade = grade;
                student = await studentRepository.Post(student);
                studentDTO = mapper.Map<StudentDTO>(student);
                return studentDTO;
            }
            return null;
        }
        public async Task<StudentDTO> Put(int id, StudentDTO studentDTO)
        {
            var student = await studentRepository.Get(id);
            var grades = await gradeRepository.GetByProperty(t => t.GradeValue == studentDTO.GradeValue);
            var grade = grades.FirstOrDefault();
            if (student == null || grade == null) return null;
            studentDTO.Id = id;
            student = mapper.Map<Student>(studentDTO);
            student.Grade = grade;
            studentRepository.Put(student);
            student = await studentRepository.Get(id);
            var studentDTOUpdated = mapper.Map<StudentDTO>(student);
            return studentDTOUpdated;
        }
        public async Task Delete(int id)
        {
            await studentRepository.Delete(id);
        }
    }
}
