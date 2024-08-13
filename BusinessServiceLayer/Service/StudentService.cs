using AutoMapper;
using BusinessServiceLayer.DTO;
using BusinessServiceLayer.Validation;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer;
using RepositoryLayer.Entity;
using RepositoryLayer.EntityRepo;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace BusinessServiceLayer.Service
{
    public class StudentService
    {
        private readonly StudentRepository studentRepository;
        private readonly CourseRepository courseRepository;
        private readonly GradeRepository gradeRepository;
        private readonly StudentAddressRepository studentAddressRepository;
        private readonly SchoolContext _context;
        private readonly IMapper mapper;
        private readonly StudentInCourseRepository studentInCourseRepository;
        private readonly ValidationService validationService;
        public StudentService(StudentRepository studentRepository, IMapper mapper, GradeRepository gradeRepository,
            StudentAddressRepository studentAddressRepository, SchoolContext context, StudentInCourseRepository studentInCourseRepository, CourseRepository courseRepository, ValidationService validationService)
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
            this.gradeRepository = gradeRepository;
            this.studentAddressRepository = studentAddressRepository;
            _context = context;
            this.studentInCourseRepository = studentInCourseRepository;
            this.courseRepository = courseRepository;
            this.validationService = validationService;
        }

        public async Task<IEnumerable<StudentDTO>> Get(int pageNumber = 1, int pageSize = 10)
        {
            var dtos = new List<StudentDTO>();
            var entities = await studentRepository.Get(null, q => q.OrderByDescending(s => s.Id), pageNumber, pageSize, s => s.Include(s => s.Address).Include(s => s.Grade).Include(s => s.Courses).ThenInclude(st => st.Course));

            foreach (var entity in entities)
            {
                var studentDto = mapper.Map<StudentDTO>(entity);
                dtos.Add(studentDto);
            }

            return dtos;
        }

        public async Task<StudentDTO> Get(int id)
        {
            var student = await studentRepository.Get(id, s => s.Include(s => s.Address).Include(s => s.Grade).Include(s => s.Courses).ThenInclude(st => st.Course));
            var studentDto = mapper.Map<StudentDTO>(student);
            return studentDto;
        }
        public async Task<StudentDTO> Post(StudentInputDTO studentInputDTO)
        {
            if (!validationService.ValidatePhoneNumber(studentInputDTO.Phone)) return new StudentDTO() { TypeError = ErrorConst.STUDENT_VALIDATION_PHONE };
            StudentAddress studentAddress = mapper.Map<StudentAddress>(studentInputDTO.Address);
            var student = mapper.Map<Student>(studentInputDTO);
            var grades = await gradeRepository.GetByProperty(t => t.GradeValue == studentInputDTO.GradeValue);
            var grade = grades.FirstOrDefault();

            if (grade == null)
            {
                if (!validationService.ValidateGradeValue(studentInputDTO.GradeValue)) return new StudentDTO() { TypeError = ErrorConst.STUDENT_VALIDATION_GRADE_VALUE };
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
            var student = await studentRepository.Get(id, s => s.Include(s => s.Address).Include(s => s.Grade));
            if (student == null) return null;
            if (!validationService.ValidatePhoneNumber(studentInputDTO.Phone)) return new StudentDTO() { TypeError = ErrorConst.STUDENT_VALIDATION_PHONE };
            var grades = await gradeRepository.GetByProperty(t => t.GradeValue == studentInputDTO.GradeValue);
            var grade = grades.FirstOrDefault();
            if (grade == null)
            {
                if (!validationService.ValidateGradeValue(studentInputDTO.GradeValue)) return new StudentDTO() { TypeError = ErrorConst.STUDENT_VALIDATION_GRADE_VALUE };
                grade = new Grade(studentInputDTO.GradeValue);
                grade = await gradeRepository.Post(grade);
                student.Grade = grade;
            }
            else
            {
                _context.Entry(grade).State = EntityState.Unchanged;
            }
            
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
        public async Task<StudentInCourseInputDTO> AddStudentIntoCourse(StudentInCourseInputDTO studentInCourseInputDTO)
        {
            var student = await studentRepository.Get(studentInCourseInputDTO.StudentId);
            var course = await courseRepository.Get(studentInCourseInputDTO.CourseId);
            if (course == null || student == null) return null;
            var studentInCourses = await studentInCourseRepository.
                GetByProperty(sc => sc.StudentId == studentInCourseInputDTO.StudentId && sc.CourseId == studentInCourseInputDTO.CourseId);
            if (studentInCourses.FirstOrDefault() != null) return null;
            _context.Entry(student).State = EntityState.Unchanged;
            _context.Entry(course).State = EntityState.Unchanged;
            var studentInCourse = mapper.Map<StudentInCourse>(studentInCourseInputDTO);
            studentInCourse = await studentInCourseRepository.Post(studentInCourse);
            return mapper.Map<StudentInCourseInputDTO>(studentInCourse);
        }
        public async Task RemoveStudentOutOfCourse(StudentInCourseInputDTO studentInCourseInputDTO)
        {
            var student = await studentRepository.Get(studentInCourseInputDTO.StudentId);
            var course = await courseRepository.Get(studentInCourseInputDTO.CourseId);
            if (course == null || student == null) return;
            var studentInCourses = await studentInCourseRepository.
                GetByProperty(sc => sc.StudentId == studentInCourseInputDTO.StudentId && sc.CourseId == studentInCourseInputDTO.CourseId);
            var studentInCourse = studentInCourses.FirstOrDefault();
            if (studentInCourse == null) return;
            await studentInCourseRepository.Delete(studentInCourse.Id);
        }
    }
}
