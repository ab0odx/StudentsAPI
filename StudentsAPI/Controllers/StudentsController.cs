using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Students_Business;

namespace StudentsAPI.Controllers
{
    [Route("api/Students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<StudentDto>> GetAllStudents() 
        {
            var students = StudentsService.GetAllStudents();

            if (students.Count == 0)
                return NotFound("No Students Were Found.");

            return Ok(students);
        }

        [HttpGet("passed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<StudentDto>> GetAllPassedStudents()
        {
            var students = StudentsService.GetAllPassedStudents();

            if (students.Count == 0)
                return NotFound("No Students Were Found.");

            return Ok(students);
        }

        [HttpGet("averageGrade")]
        public ActionResult<double> GetAverageGrade()
        {
            double averageGrade = StudentsService.GetAverageGrade();

            return Ok(averageGrade);
        }

        [HttpGet("{id:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<StudentDto> GetStudentById(int id)
        {
            if (id < 1)
                return BadRequest($"Invalid Student ID {id}");

            var student = StudentsService.GetStudentById(id);

            if (student == null)
                return NotFound("Student Not Found.");

            return Ok(student);
        }
    }
}
