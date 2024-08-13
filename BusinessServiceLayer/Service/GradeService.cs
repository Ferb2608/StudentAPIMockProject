using AutoMapper;
using BusinessServiceLayer.DTO;
using BusinessServiceLayer.Validation;
using RepositoryLayer;
using RepositoryLayer.Entity;
using RepositoryLayer.EntityRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessServiceLayer.Service
{
    public class GradeService
    {
        private readonly GradeRepository gradeRepository;
        private readonly IMapper mapper;
        private readonly ValidationService validationService;
        public GradeService(GradeRepository gradeRepository, ValidationService validationService, IMapper mapper)
        {
            this.gradeRepository = gradeRepository;
            this.validationService = validationService;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<GradeOutputDTO>> Get(int pageNumber = 1, int pageSize = 10)
        {
            var dtos = new List<GradeOutputDTO>();
            var entities = await gradeRepository.Get(pageNumber: pageNumber, pageSize: pageSize);
            foreach (var entity in entities)
            {
                var gradeOutputDTO = mapper.Map<GradeOutputDTO>(entity);
                dtos.Add(gradeOutputDTO);
            }

            return dtos;
        }
        public async Task<GradeOutputDTO> Get(int id)
        {
            var grade = await gradeRepository.Get(id);
            var gradeOutputDTO = mapper.Map<GradeOutputDTO>(grade);
            return gradeOutputDTO;
        }
        public async Task<GradeOutputDTO> Post(GradeInputDTO gradeInputDTO)
        {
            if (!validationService.ValidateGradeValue(gradeInputDTO.GradeValue)) return null;
            var grades = await gradeRepository.GetByProperty(gr => gr.GradeValue == gradeInputDTO.GradeValue);
            var grade = grades.FirstOrDefault();
            if (grade != null) return null;
            grade = mapper.Map<Grade>(gradeInputDTO);
            grade = await gradeRepository.Post(grade);
            return mapper.Map<GradeOutputDTO>(grade);
        }
        public async Task<GradeOutputDTO> Put(int id, GradeInputDTO gradeInputDTO)
        {
            var grade = await gradeRepository.Get(id);
            if (grade == null || !validationService.ValidateGradeValue(gradeInputDTO.GradeValue)) return null;
            grade = mapper.Map<Grade>(gradeInputDTO);
            grade.Id = id;
            gradeRepository.Put(grade);
            return mapper.Map<GradeOutputDTO>(grade);
        }
        public async Task Delete(int id)
        {
            var grade = await gradeRepository.Get(id);
            if (grade == null) return;
            await gradeRepository.Delete(id);
        }

    }
}
