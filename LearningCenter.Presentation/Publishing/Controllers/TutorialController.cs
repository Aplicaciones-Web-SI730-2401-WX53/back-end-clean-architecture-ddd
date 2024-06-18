using System.Net.Mime;
using _1_API.Response;
using AutoMapper;
using Domain;
using LearningCenter.Domain.Publishing.Models.Queries;
using LearningCenter.Presentation.Security.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Request;

namespace Presentation.Controllers;

/// <summary>
/// 
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TutorialController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ITutorialCommandService _tutorialCommandService;
    private readonly ITutorialQueryService _tutorialQueryService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tutorialQueryService"></param>
    /// <param name="tutorialCommandService"></param>
    /// <param name="mapper"></param>
    public TutorialController(ITutorialQueryService tutorialQueryService, ITutorialCommandService tutorialCommandService,
        IMapper mapper)
    {
        _tutorialQueryService = tutorialQueryService;
        _tutorialCommandService = tutorialCommandService;
        _mapper = mapper;
    }


    // GET: api/Tutorial
    /// <summary>
    /// Get all tutorial avalibles.
    /// </summary>
    /// <remarks>
    ///  Example:
    ///  GET api/Tutorial
    ///   </remarks>
    /// <returns>
    /// the list of tutorials
    /// </returns>

    [HttpGet]
    [ProducesResponseType(typeof(List<TutorialResponse>), 200)]
    [ProducesResponseType(typeof(void),statusCode: StatusCodes.Status404NotFound)]
    [ProducesResponseType(500)]
    [Produces(MediaTypeNames.Application.Json)]
    [CustomAuthorize("mkt")]
    public async Task<IActionResult> GetAsync()
    {
        var result = await _tutorialQueryService.Handle(new GetAllTutorialsQuery());
        
        if (result != null && result.Count == 0) return NotFound();

        return Ok(result);
    }

    // GET: api/Tutorial/Search
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="year"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("Search")]
    [CustomAuthorize("mkt")]
    public async Task<IActionResult> GetSearchAsync(string? name, int? year)
    {
        //var data = await _tutorialRepository.GetSearchAsync(name, year);

        var result = await _tutorialQueryService.Handle(new GetAllTutorialsQuery());
        if (result != null && result.Count == 0) return NotFound();

        return Ok(result);
    }

    // GET: api/Tutorial/5
    /// <summary>
    /// Gets a Tutorial by ID.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/Tutorial/{id}
    ///
    /// </remarks>
    /// <param name="id">The ID of the tutorial</param>
    /// <returns>The tutorial with the specified ID</returns>
    /// <response code="200">Returns the tutorial</response>
    /// <response code="404">If the tutorial is not found</response>
    [HttpGet("{id}", Name = "GetAsync")]
    [ProducesResponseType(typeof(TutorialResponse), 200)]
    [ProducesResponseType(typeof(void),statusCode: StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(void),StatusCodes.Status500InternalServerError)]
    [CustomAuthorize("mkt")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var result = await _tutorialQueryService.Handle(new GetTutorialsByIdQuery(id));

        if (result==null) StatusCode(StatusCodes.Status404NotFound);
        
        return Ok(result);
    }

    // POST: api/Tutorial
    /// <summary>
    /// Creates a new tutorial.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/tutorial
    ///     {
    ///        "name": "New tutorial",
    ///        "description": ""
    ///     }
    /// 
    /// </remarks>
    /// <param name="command"></param>
    /// <returns>A newly created tutorial</returns>
    /// <response code="201">Returns the newly created tutorial</response>
    /// <response code="409">Error validating data</response>
    /// <response code="500">Unexpected error</response>
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(void),StatusCodes.Status500InternalServerError)]
    [CustomAuthorize("admin")]
    public async Task<IActionResult> PostAsync([FromBody] CreateTutorialCommand command)
    {
        if (!ModelState.IsValid) return BadRequest();


        var result = await _tutorialCommandService.Handle(command);

        if (result > 0)
            return StatusCode(StatusCodes.Status201Created, result);

        return BadRequest();
    }

    // PUT: api/Tutorial/5
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] UpdateTutorialCommand command)
    {
        command.Id = id;
        if (!ModelState.IsValid) return StatusCode(StatusCodes.Status400BadRequest);

        var result = await _tutorialCommandService.Handle(command);

        return Ok();
    }

    // DELETE: api/Tutorial/5
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        DeleteTutorialCommand command = new DeleteTutorialCommand { Id = id };

        var result = await _tutorialCommandService.Handle(command);

        return Ok();
    }
}