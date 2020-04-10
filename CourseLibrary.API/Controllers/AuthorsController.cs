using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Helpers;
using CourseLibrary.API.Models;
using CourseLibrary.API.ResourceParameters;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;

        public AuthorsController(ICourseLibraryRepository courseLibraryRepository,IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository ??
                                       throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ??
                      throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors(
            [FromQuery] AuthorResourceParameter authorResourceParameter)
        {
            var authorsFromRepo = _courseLibraryRepository.GetAuthors(authorResourceParameter);
            var authors = _mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo);
            return Ok(authors);
        }

        [HttpGet("{authorId}" ,Name = "GetAuthor")]
        public IActionResult GetAuthor(Guid authorId)
        {
            var authorFromRepo = _courseLibraryRepository.GetAuthor(authorId);
            if (authorFromRepo == null) return NotFound();
            return Ok(authorFromRepo);
        }
        [HttpPost]
        public ActionResult<AuthorDto> CreateAuthor([FromBody]AuthorCreationDto authorDto)
        {
            var author = _mapper.Map<Author>(authorDto);
            _courseLibraryRepository.AddAuthor(author);
            _courseLibraryRepository.Save();
            var authorToReturn = _mapper.Map<AuthorDto>(author);
            return CreatedAtRoute("GetAuthor", new {authorId = authorToReturn.Id}, authorToReturn);
        }
    }
}