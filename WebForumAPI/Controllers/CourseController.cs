using System.Security.Claims;
using Application.Services;
using Core.Entities;
using Core.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repositiries;

namespace WebForumAPI.Controllers;

[ApiController]
[Route("api")]
public class CourseController : ControllerBase
{
  private readonly CourseService _courseService;

  public CourseController(CourseService courseService)
  {
    _courseService = courseService;
  }
  
  [HttpGet("courses")]
  public async Task<ActionResult<List<Course>>> Get()
  {
    var course = await _courseService.Get();
    if (course.Count == 0)
    {
      return NotFound();
    }
    return Ok(course);
  }

  [HttpGet("courses/{id}")]
  public async Task<ActionResult<Course>> GetById([FromRoute] Guid id)
  {
    var course = await _courseService.GetById(id);
    if (course == null)
    {
      return NotFound();
    }
    return Ok(course);
  }

  [HttpGet("courses/by-title")]
  public async Task<ActionResult<Course>> GetByTitle([FromBody] string title)
  {
    var course = await _courseService.GetByTitle(title);
    if (course == null)
    {
      return NotFound();
    }
    return Ok(course);
  }

  [HttpGet("courses/by-lector/{lector}")]
  public async Task<ActionResult<List<Course>>> GetByLector(Guid lector)
  {
    var courses = await _courseService.GetByLector(lector);
    if (courses.Count == 0)
    {
      return NotFound();
    }
    return Ok(courses);
  }

  [Authorize(Roles = "admin, lecture")]
  [HttpPost("add/course")]
  public async Task<ActionResult> Add([FromBody] CourseRequest courseRequest)
  {
    await _courseService.Add(courseRequest.Title, courseRequest.Description, courseRequest.Creator,
      courseRequest.Categories);
    return Created();
  }

  [Authorize]
  [HttpDelete("delete/course/{id}")]
  public async Task<ActionResult> Delete(Guid id)
  {
    var lectureId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var course = await _courseService.GetById(id);
    if (course.Creator.Id.ToString() != lectureId) return Forbid();
    await _courseService.Delete(id);
    return Ok();
  }

  [Authorize]
  [HttpPatch("update/courses/title/{id}")]
  public async Task<ActionResult> UpdateTitle([FromRoute] Guid id, [FromBody] CourseUpdateRequest course)
  {
    var lectureId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var c = await _courseService.GetById(id);
    if (c.Creator.Id.ToString() != lectureId) return Forbid();
    await _courseService.UpdateTitle(id, course.Text);
    return Ok();
  }

  [Authorize]
  [HttpPatch("update/course/description/{id}")]
  public async Task<ActionResult> UpdateDescription([FromRoute] Guid id, CourseUpdateRequest course)
  {
    var lectureId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var c = await _courseService.GetById(id);
    if (c.Creator.Id.ToString() != lectureId) return Forbid();
    await _courseService.UpdateDescription(id, course.Text);
    return Ok();
  }

  [Authorize]
  [HttpPatch("update/course/categories/{id}")]
  public async Task<ActionResult> UpdateCategories([FromRoute] Guid id,
    CourseCategoriesUpdateRequest course)
  {
    var lectureId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var c = await _courseService.GetById(id);
    if (c.Creator.Id.ToString() != lectureId) return Forbid();
    await _courseService.UpdateCategories(id, course.Categories);
    return Ok();
  }
}