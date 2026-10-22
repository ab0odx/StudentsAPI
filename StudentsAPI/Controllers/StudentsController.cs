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

        [HttpGet("{id:int:min(1)}", Name = "GetStudentById")]
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

        [HttpPost(Name = "AddStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<StudentDto> PostStudent(StudentDto student)
        {
            if (student == null || string.IsNullOrEmpty(student.Name))
                return BadRequest("Invalid Student Name.");

            int newStudentId = StudentsService.AddStudent(student);
            student.Id = newStudentId;
            
            return CreatedAtRoute("GetStudentById", new { id = newStudentId }, student);
        }

        [HttpDelete("{id:int:min(1)}", Name = "DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<StudentDto> DeleteStudent(int id)
        {
            if (id < 1)
                return BadRequest("Invalid Student ID.");
            
            bool deleted = StudentsService.DeleteStudent(id);
            
            if (!deleted)
                return NotFound($"Student with ID {id} was not found.");

            return NoContent();
        }

        [HttpPut("{id:int:min(1)}", Name = "UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<StudentDto> UpdateStudent(int id, StudentDto student)
        {
            if(id < 1)
                return BadRequest("Invalid Student ID.");
            
            bool updated = StudentsService.UpdateStudent(id, student);
            
            if (!updated)
                return NotFound($"Student with ID {id} was not found.");
            
            return NoContent();
        }
    }
}
