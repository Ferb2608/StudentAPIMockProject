using BusinessServiceLayer.Service;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer;

namespace PresentationLayer.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StudentAddressController : Controller
    {
        private readonly StudentAddressService studentAddressService;
        public StudentAddressController(StudentAddressService studentAddressService)
        {
            this.studentAddressService = studentAddressService;
        }
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return Ok(await studentAddressService.Get(pageNumber: pageNumber, pageSize: pageSize));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await studentAddressService.Get(id));
        }
    }
}
