using AutoMapper;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServiceLayer
{
    public class StudentAddressService
    {
        private readonly StudentAddressRepository studentAddressRepository;
        private readonly IMapper mapper;
        public StudentAddressService(StudentAddressRepository studentAddressRepository, IMapper mapper)
        {
            this.studentAddressRepository = studentAddressRepository;
            this.mapper = mapper;
        }   
        public async Task<IEnumerable<AddressDTO>> Get(int pageNumber = 1, int pageSize = 10)
        {
            var dtos = new List<AddressDTO>();
            var entities = await studentAddressRepository.Get(null, q => q.OrderByDescending(s => s.Id), pageNumber: pageNumber, pageSize: pageSize);
            foreach (var entity in entities)
            {
                var addressDto = mapper.Map<AddressDTO>(entity);
                dtos.Add(addressDto);
            }

            return dtos;
        }
    }
}
