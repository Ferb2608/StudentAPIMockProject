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
    public class CourseService
    {
        private readonly CourseRepository courseRepository;
        private readonly StudentRepository studentRepository;
        private readonly SchoolContext _context;
        private readonly IMapper mapper;
        public CourseService(CourseRepository courseRepository, IMapper mapper,
            StudentRepository studentRepository, SchoolContext context)
        {
            this.courseRepository = courseRepository;
            this.mapper = mapper;
            this.studentRepository = studentRepository;
            _context = context;
        }

        public async Task<IEnumerable<CourseDTO>> Get(int pageNumber = 1, int pageSize = 10)
        {
            var dtos = new List<CourseDTO>();
            var entities = await courseRepository.Get(null, q => q.OrderByDescending(s => s.Id), pageNumber: pageNumber, pageSize: pageSize);
            foreach (var entity in entities)
            {
                var courseDto = mapper.Map<CourseDTO>(entity);
                dtos.Add(courseDto);
            }
            return dtos;
        }

        public async Task<CourseDTO> Get(int id)
        {
            var course = await courseRepository.Get(id: id);
            var courseDto = mapper.Map<CourseDTO>(course);
            return courseDto;
        }
        public async Task<CourseDTO> Post(CourseDTO courseDTO)
        {
            var course = mapper.Map<Course>(courseDTO);
            course = await courseRepository.Post(course);
            return mapper.Map<CourseDTO>(course);
        }
        public async Task<CourseDTO> Put(int id, CourseDTO courseDTO)
        {
            var course = await courseRepository.Get(id);
            course = mapper.Map<Course>(courseDTO);
            courseRepository.Put(course);
            course = await courseRepository.Get(id);
            var courseUpdate = mapper.Map<CourseDTO>(course);
            return courseUpdate;
        }
        public async Task Delete(int id)
        {
            var student = await courseRepository.Get(id);
            await courseRepository.Delete(id);
        }
    }
}
