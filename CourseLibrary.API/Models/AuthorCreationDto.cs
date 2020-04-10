using System;

namespace CourseLibrary.API.Models
{
    public class AuthorCreationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirthDateTime { get; set; }
        public string MainCategory { get; set; }

    }
}