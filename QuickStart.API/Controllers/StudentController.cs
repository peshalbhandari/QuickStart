using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickStart.API.DTOs;
using QuickStart.API.Models;

namespace QuickStart.API.Controllers
{
    [ApiController]
    [Route("api/student")]
    public class StudentController : ControllerBase
    {
        private readonly RosterDbContext dbContext;

        public StudentController(RosterDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        /// <summary>
        /// GET (Read all) /students
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerator<StudentDTO>>> GetAll()
        {
            var students =await dbContext.Student.ToArrayAsync();
            return Ok(students.Select(s=>ToDTO(s)));
        }



        /// <summary>
        /// GET Student by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An object of type StudentDTO</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(StudentDTO),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var student = await dbContext.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(ToDTO(student));
        }

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status409Conflict)]
        //public async Task<ActionResult<StudentDTO>> Create([FromBody]StudentDTO student)
        //{
        //    if (string.IsNullOrEmpty(student.Name))
        //    {
        //        return BadRequest();
        //    }
        //    var @class = await dbContext.Class.FindAsync(student.ClassId);
        //    if (@class == null)
        //        return NotFound();

        //    var existing = await dbContext.Student.FindAsync(student.Id);
        //    if (existing != null)
        //    {
        //        return Conflict();
        //    }
            
        //}

        private static StudentDTO ToDTO(Student student)
        {
            return new StudentDTO
            {
                Id = student.Id,
                Name = student.Name,
                ClassId = student.Class.Id,
                TeacherId = student.Class.Teacher.Id,
                SchoolId = student.Class.Teacher.School.Id
            };
        }
    }
}