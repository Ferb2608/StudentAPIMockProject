using BusinessServiceLayer.DTO;
using BusinessServiceLayer.Service;
using Microsoft.AspNetCore.Mvc;
using WebAPISample;

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
            var grade = await gradeService.Get(id);
            if (grade == null) return NotFound();
            return Ok(grade);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GradeInputDTO gradeInputDTO)
        {
            var studentDTO = await gradeService.Post(gradeInputDTO);
            if (studentDTO == null)
            {
                ValidationModel validationModel = new ValidationModel("Save Grade", "Grade value is " + gradeInputDTO.GradeValue, "This grade value is already exist or not valid (2 digits and less than 12), please try again!");
                return BadRequest(validationModel);
            }
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
            ValidationModel validationModel = new ValidationModel("Update Grade", "Grade value is " + gradeInputDTO.GradeValue, "This grade value is already exist or not valid (2 digits and less than 12), please try again!");
            return BadRequest(validationModel);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await gradeService.Delete(id);
            return NoContent();
        }
    }
}
