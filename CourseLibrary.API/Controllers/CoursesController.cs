using System;
using System.Collections.Generic;
using AutoMapper;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authors/{authorId}/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;

        public CoursesController(ICourseLibraryRepository courseLibraryRepository,IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository ??
                                       throw new ArgumentException(nameof(courseLibraryRepository));
            _mapper = mapper ??
                      throw new ArgumentException(nameof(mapper));
        }
        [HttpGet]
        public ActionResult<IEnumerable<CourseDto>> GetCourses(Guid authorId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var coursesForAuthorFromRepo =
                _mapper.Map<IEnumerable<CourseDto>>(_courseLibraryRepository.GetCourses(authorId));
            return Ok(coursesForAuthorFromRepo);
        }
        [HttpGet("{courseId}")]
        public ActionResult<CourseDto> GetCourse(Guid authorId, Guid courseId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var courseForAuthorFromRepo =
                _mapper.Map<CourseDto>(_courseLibraryRepository.GetCourse(authorId, courseId));
            if (courseForAuthorFromRepo == null) return NotFound();
            return Ok(courseForAuthorFromRepo);


        }
    }
}