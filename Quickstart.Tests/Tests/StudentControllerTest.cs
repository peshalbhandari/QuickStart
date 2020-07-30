using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using QuickStart.API;
using QuickStart.API.DTOs;
using QuickStart.API.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Quickstart.Tests.Tests
{
    [Collection("Integration Tests")]
    public class StudentControllerTest
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private Student _modelStudent;

        public StudentControllerTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            // This fetches the same single lifetime instantiation used by Controller classes
            var dbContext = _factory.Services.GetRequiredService<RosterDbContext>();

            // Seed in-memory database with some data needed for tests
            var school = new School
            {
                Id = 1,
                Name = "School of Hard Knocks",
                City = "Life",
                State = "Madness"
            };
            dbContext.School.Add(school);
            var teacher = new Teacher
            {
                Id = 1,
                Name = "Mrs. Stricter",
                School = school
            };
            dbContext.Teacher.Add(teacher);
            var @class = new Class
            {
                Id = 1,
                Name = "Fifth Grade Class",
                Teacher = teacher
            };
            dbContext.Class.Add(@class);
            _modelStudent = new Student
            {
                Id = 1,
                Name = "Jim Bob",
                Class = @class
            };
            dbContext.Student.Add(_modelStudent);
            dbContext.SaveChanges();
        }
        [Fact]
        public async Task GetStudent_ReturnSuccessAndStudent()
        {
            //Arrange
            var client = _factory.CreateClient();
            //Act
            var response = await client.GetAsync("api/student/1");
            //Assert
            response.EnsureSuccessStatusCode();
            Assert.NotNull(response.Content);
            var responseStudent = JsonSerializer.Deserialize<StudentDTO>(
                await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Assert.NotNull(responseStudent);
            Assert.Equal(_modelStudent.Id, responseStudent.Id);
            Assert.Equal(_modelStudent.Name, responseStudent.Name);
            Assert.Equal(_modelStudent.Class.Id, responseStudent.ClassId);
            Assert.Equal(_modelStudent.Class.Teacher.Id, responseStudent.TeacherId);
            Assert.Equal(_modelStudent.Class.Teacher.School.Id, responseStudent.SchoolId);
        }
    }
}
