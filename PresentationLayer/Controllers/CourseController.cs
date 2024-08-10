using BusinessServiceLayer.DTO;
using BusinessServiceLayer.Service;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly CourseService courseService;
        public CourseController(CourseService courseService)
        {
            this.courseService = courseService;
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return Ok(await courseService.Get(pageNumber: pageNumber, pageSize: pageSize));
        }

        //GET api/<StudentsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await courseService.Get(id));
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CourseDTO course)
        {
            var studentDTO = await courseService.Post(course);
            return CreatedAtAction("Post", studentDTO);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CourseDTO course)
        {
            var result = await courseService.Put(id, course);
            if (result != null)
            {
                return NoContent();
            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await courseService.Delete(id);
            return NoContent();
        }
    }
}
