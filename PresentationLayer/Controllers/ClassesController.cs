using BusinessServiceLayer.DTO;
using BusinessServiceLayer.Service;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly ClassService classService;
        public ClassesController(ClassService classService)
        {
            this.classService = classService;
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return Ok(await classService.Get(pageNumber: pageNumber, pageSize: pageSize));
        }

        //GET api/<StudentsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await classService.Get(id));
        }
        //[HttpPost]
        //public async Task<ActionResult> Post([FromBody] StudentInputDTO student)
        //{
        //    var studentDTO = await classService.Post(student);
        //    return CreatedAtAction("Post", studentDTO);
        //}
        //[HttpPut("{id}")]
        //public async Task<ActionResult> Put(int id, [FromBody] StudentInputDTO student)
        //{
        //    var result = await classService.Put(id, student);
        //    if (result != null)
        //    {
        //        return NoContent();
        //    }
        //    return BadRequest();
        //}
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    await classService.Delete(id);
        //    return NoContent();
        //}
    }
}
