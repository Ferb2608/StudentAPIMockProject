using BusinessServiceLayer.DTO;
using BusinessServiceLayer.Service;
using BusinessServiceLayer.Validation;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;

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
            if (student == null) return NotFound();
            return Ok(student);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] StudentInputDTO student)
        {
            var studentDTO = await studentService.Post(student);
            if (!string.IsNullOrEmpty(studentDTO.TypeError))
            {
                ValidationModel validationModel = null;
                if (studentDTO.TypeError == ErrorConst.STUDENT_VALIDATION_PHONE)
                {
                    validationModel = new ValidationModel("Save student", "Phone number is " + student.Phone, "Phone number must be 10 digits and belong to VietNamese's phone number!");
                }
                else if (studentDTO.TypeError == ErrorConst.STUDENT_VALIDATION_GRADE_VALUE)
                {
                    validationModel = new ValidationModel("Save student", "Grade Value is " + student.GradeValue, "Grade Value must be 1-2 digits and less than 12!");
                }
                return BadRequest(validationModel);
            }
            return CreatedAtAction("Post", studentDTO);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] StudentInputDTO student)
        {
            var studentDTO = await studentService.Put(id, student);
            if (!string.IsNullOrEmpty(studentDTO.TypeError))
            {
                ValidationModel validationModel = null;
                if (studentDTO.TypeError == ErrorConst.STUDENT_VALIDATION_PHONE)
                {
                    validationModel = new ValidationModel("Save student", "Phone number is " + student.Phone, "Phone number must be 10 digits and belong to VietNamese's phone number!");
                }
                else if (studentDTO.TypeError == ErrorConst.STUDENT_VALIDATION_GRADE_VALUE)
                {
                    validationModel = new ValidationModel("Save student", "Grade Value is " + student.GradeValue, "Grade Value must be 1-2 digits and less than 12!");
                }
                return BadRequest(validationModel);
            }
            return NoContent();
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
            if (studentInCourse == null)
            {
               ValidationModel validationModel = new ValidationModel
                    ("Add Student into Course", "Student id: " + studentInCourseInputDTO.StudentId + "Course id: " + studentInCourseInputDTO.CourseId, 
                    "Student id and Course id is not exist or Student already in this Course");
                return BadRequest(validationModel);
            }
            return CreatedAtAction("AddStudentIntoCourse", studentInCourse);
        }
        
        [HttpDelete("removeStudentOutOfCourse")]
        public async Task<ActionResult> RemoveStudentOutOfCourse(StudentInCourseInputDTO studentInCourseInputDTO)
        {
            await studentService.RemoveStudentOutOfCourse(studentInCourseInputDTO);
            return NoContent();
        }
    }
}
