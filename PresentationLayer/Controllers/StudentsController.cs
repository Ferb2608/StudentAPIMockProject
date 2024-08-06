using BusinessServiceLayer;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer;

namespace WebAPISample.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentService studentService;
        public StudentsController(StudentService studentService)
        {
            this.studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return Ok(await studentService.Get(pageNumber: pageNumber, pageSize: pageSize));
        }

        //GET api/<StudentsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await studentService.Get(id));
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] StudentDTO student)
        {
            var studentDTO = await studentService.Post(student);
            return CreatedAtAction("Post", studentDTO);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] StudentDTO student)
        {
            var result = await studentService.Put(id, student);
            if (result != null)
            {
                return NoContent();
            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await studentService.Delete(id);
            return NoContent();
        }
    }
}
