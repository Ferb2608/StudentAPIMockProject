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

        //[HttpGet]
        //public async Task<ActionResult> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        //{
        //    return Ok(await studentService.Get(pageNumber: pageNumber, pageSize: pageSize));
        //}

        //GET api/<StudentsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await studentService.Get(id));
        }
        //[HttpPost]
        //public async Task<ActionResult> Post([FromBody] Student student)
        //{
        //    var studentDTO = await studentService.Post(student);
        //    return CreatedAtAction("Post", studentDTO);
        //}
        //[HttpPut]
        //public ActionResult Put([FromBody] Student student)
        //{
        //    studentService.Put(student);
        //    return NoContent();
        //}
        //[HttpDelete("{id}")]
        //public ActionResult Delete(int id) 
        //{ 
        //    studentService.Delete(id);
        //    return NoContent();
        //}
    }
}
