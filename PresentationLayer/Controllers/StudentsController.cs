using BusinessServiceLayer.DTO;
using BusinessServiceLayer.Service;
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

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var student = await studentService.Get(id);
            return Ok(await studentService.Get(id));
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] StudentInputDTO student)
        {
            var studentDTO = await studentService.Post(student);
            return CreatedAtAction("Post", studentDTO);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] StudentInputDTO student)
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
        [HttpPost("addStudentIntoCourse")]
        public async Task<ActionResult> AddStudentIntoCourse(StudentInCourseInputDTO studentInCourseInputDTO)
        {
            var studentInCourse = await studentService.AddStudentIntoCourse(studentInCourseInputDTO);
            return CreatedAtAction("AddStudentIntoCourse", studentInCourse);
        }
        //[HttpPut("updateStudentIntoCourse")]
        //public async Task<ActionResult> UpdateStudentIntoCourse(StudentInCourseInputDTO studentInCourseInputDTO)
        //{
        //    var studentInCourse = await studentService.UpdateStudentIntoCourse(studentInCourseInputDTO);
        //    return NoContent();
        //}
        [HttpDelete("removeStudentOutOfCourse")]
        public async Task<ActionResult> RemoveStudentOutOfCourse(StudentInCourseInputDTO studentInCourseInputDTO)
        {
            await studentService.RemoveStudentOutOfCourse(studentInCourseInputDTO);
            return NoContent();
        }
    }
}
