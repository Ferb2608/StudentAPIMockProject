using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer;

namespace BusinessServiceLayer
{
    public class StudentService
    {
        private readonly StudentRepository studentRepository;
        private readonly GradeRepository gradeRepository;
        private readonly StudentAddressRepository studentAddressRepository;
        private readonly SchoolContext _context;
        private readonly IMapper mapper;
        public StudentService(StudentRepository studentRepository, IMapper mapper, GradeRepository gradeRepository,
            StudentAddressRepository studentAddressRepository, SchoolContext context)
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
            this.gradeRepository = gradeRepository;
            this.studentAddressRepository = studentAddressRepository;
            _context = context;
        }

        public async Task<IEnumerable<StudentDTO>> Get(int pageNumber = 1, int pageSize = 10)
        {
            var dtos = new List<StudentDTO>();
            var entities = await studentRepository.Get(null, q => q.OrderByDescending(s => s.Id), pageNumber: pageNumber, pageSize: pageSize,
                                                s => s.Grade, s => s.Address);
            foreach (var entity in entities)
            {
                var studentDto = mapper.Map<StudentDTO>(entity);
                dtos.Add(studentDto);
            }

            return dtos;
        }

        public async Task<StudentDTO> Get(int id)
        {
            var student = await studentRepository.Get(id: id, s => s.Grade, s => s.Address);
            var studentDto = mapper.Map<StudentDTO>(student);
            return studentDto;
        }
        public async Task<StudentDTO> Post(StudentInputDTO studentInputDTO)
        {
            StudentAddress studentAddress = mapper.Map<StudentAddress>(studentInputDTO.Address);
            var student = mapper.Map<Student>(studentInputDTO);
            var grades = await gradeRepository.GetByProperty(t => t.GradeValue == studentInputDTO.GradeValue);
            var grade = grades.FirstOrDefault();

            if (grade == null)
            {
                grade = new Grade(studentInputDTO.GradeValue);
                grade = await gradeRepository.Post(grade);
            }
            else
            {
                _context.Entry(grade).State = EntityState.Unchanged;
            }

            student.GradeId = grade.Id;
            var address = await studentAddressRepository.Post(studentAddress);
            student.Address = address;
            student = await studentRepository.Post(student);
            return mapper.Map<StudentDTO>(student);
        }
        public async Task<StudentDTO> Put(int id, StudentInputDTO studentInputDTO)
        {
            var student = await studentRepository.Get(id, s => s.Grade, s => s.Address);
            var grades = await gradeRepository.GetByProperty(t => t.GradeValue == studentInputDTO.GradeValue);
            var grade = grades.FirstOrDefault();
            if (student == null) return null;
            if (grade == null)
            {
                grade = new Grade(studentInputDTO.GradeValue);
                grade = await gradeRepository.Post(grade);
                student.Grade = grade;
            } else
            {
                _context.Entry(grade).State = EntityState.Unchanged;
            }
            //studentDTO.Id = id;
            var addressUpdate = mapper.Map<StudentAddress>(studentInputDTO.Address);
            addressUpdate.Id = student.AddressId;
            student = mapper.Map<Student>(studentInputDTO);
            student.Id = id;
            student.Address = addressUpdate;
            student.GradeId = grade.Id;
            studentRepository.Put(student);
            student = await studentRepository.Get(id);
            var studentDTOUpdated = mapper.Map<StudentDTO>(student);
            return studentDTOUpdated;
        }
        public async Task Delete(int id)
        {
            var student = await studentRepository.Get(id);
            var addressId = student.AddressId;
            await studentRepository.Delete(id);
            await studentAddressRepository.Delete(addressId);
        }
    }
}
