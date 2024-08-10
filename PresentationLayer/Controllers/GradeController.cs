using BusinessServiceLayer;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer;

namespace PresentationLayer.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GradeController : Controller
    {
        private readonly GradeService gradeService;
        public GradeController(GradeService gradeService)
        {
            this.gradeService = gradeService;
        }
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return Ok(await gradeService.Get(pageNumber: pageNumber, pageSize: pageSize));
        }
        [HttpGet("id")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await gradeService.Get(id));
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GradeInputDTO gradeInputDTO)
        {
            var studentDTO = await gradeService.Post(gradeInputDTO);
            return CreatedAtAction("Post", gradeInputDTO);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] GradeInputDTO gradeInputDTO)
        {
            var result = await gradeService.Put(id, gradeInputDTO);
            if (result != null)
            {
                return NoContent();
            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await gradeService.Delete(id);
            return NoContent();
        }
    }
}
