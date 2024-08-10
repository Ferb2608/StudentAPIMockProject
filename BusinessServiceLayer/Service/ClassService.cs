using AutoMapper;
using BusinessServiceLayer.DTO;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using RepositoryLayer.EntityRepo;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServiceLayer.Service
{
    public class ClassService
    {
        private readonly ClassRepository classRepository;
        private readonly GradeRepository gradeRepository;
        private readonly SchoolContext _context;
        private readonly IMapper mapper;
        public ClassService(ClassRepository classRepository, IMapper mapper, GradeRepository gradeRepository,
            SchoolContext context)
        {
            this.classRepository = classRepository;
            this.mapper = mapper;
            this.gradeRepository = gradeRepository;
            _context = context;
        }

        public async Task<IEnumerable<ClassDTO>> Get(int pageNumber = 1, int pageSize = 10)
        {
            var dtos = new List<ClassDTO>();
            var entities = await classRepository.Get(null, q => q.OrderByDescending(s => s.Id), pageNumber: pageNumber, pageSize: pageSize);
            foreach (var entity in entities)
            {
                var classDto = mapper.Map<ClassDTO>(entity);
                dtos.Add(classDto);
            }
            return dtos;
        }

        //public async Task<StudentDTO> Get(int id)
        //{
        //    var student = await classRepository.Get(id: id, s => s.Grade, s => s.Address);
        //    var studentDto = mapper.Map<StudentDTO>(student);
        //    return studentDto;
        //}
        //public async Task<StudentDTO> Post(StudentInputDTO studentInputDTO)
        //{
        //    StudentAddress studentAddress = mapper.Map<StudentAddress>(studentInputDTO.Address);
        //    var student = mapper.Map<Student>(studentInputDTO);
        //    var grades = await gradeRepository.GetByProperty(t => t.GradeValue == studentInputDTO.GradeValue);
        //    var grade = grades.FirstOrDefault();

        //    if (grade == null)
        //    {
        //        grade = new Grade(studentInputDTO.GradeValue);
        //        grade = await gradeRepository.Post(grade);
        //    }
        //    else
        //    {
        //        _context.Entry(grade).State = EntityState.Unchanged;
        //    }

        //    student.GradeId = grade.Id;
        //    var address = await studentAddressRepository.Post(studentAddress);
        //    student.Address = address;
        //    student = await classRepository.Post(student);
        //    return mapper.Map<StudentDTO>(student);
        //}
        //public async Task<StudentDTO> Put(int id, StudentInputDTO studentInputDTO)
        //{
        //    var student = await classRepository.Get(id, s => s.Grade, s => s.Address);
        //    var grades = await gradeRepository.GetByProperty(t => t.GradeValue == studentInputDTO.GradeValue);
        //    var grade = grades.FirstOrDefault();
        //    if (student == null) return null;
        //    if (grade == null)
        //    {
        //        grade = new Grade(studentInputDTO.GradeValue);
        //        grade = await gradeRepository.Post(grade);
        //        student.Grade = grade;
        //    }
        //    else
        //    {
        //        _context.Entry(grade).State = EntityState.Unchanged;
        //    }
        //    //studentDTO.Id = id;
        //    var addressUpdate = mapper.Map<StudentAddress>(studentInputDTO.Address);
        //    addressUpdate.Id = student.AddressId;
        //    student = mapper.Map<Student>(studentInputDTO);
        //    //student.Id = id;
        //    student.Address = addressUpdate;
        //    student.GradeId = grade.Id;
        //    classRepository.Put(student);
        //    student = await classRepository.Get(id);
        //    var studentDTOUpdated = mapper.Map<StudentDTO>(student);
        //    return studentDTOUpdated;
        //}
        //public async Task Delete(int id)
        //{
        //    var student = await classRepository.Get(id);
        //    var addressId = student.AddressId;
        //    await classRepository.Delete(id);
        //    await studentAddressRepository.Delete(addressId);
        //}
    }
}
