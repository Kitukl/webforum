using Application.Services;
using Core.Entities;
using Core.Requests;
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

  [HttpPost("add/course")]
  public async Task<ActionResult> Add([FromBody] CourseRequest courseRequest)
  {
    await _courseService.Add(courseRequest.Title, courseRequest.Description, courseRequest.Creator,
      courseRequest.Categories);
    return Created();
  }

  [HttpDelete("delete/course/{id}")]
  public async Task<ActionResult> Delete(Guid id)
  {
    await _courseService.Delete(id);
    return Ok();
  }

  [HttpPatch("update/courses/title/{id}")]
  public async Task<ActionResult> UpdateTitle([FromRoute] Guid id, [FromBody] CourseUpdateRequest course)
  {
    await _courseService.UpdateTitle(id, course.Text);
    return Ok();
  }

  [HttpPatch("update/course/description/{id}")]
  public async Task<ActionResult> UpdateDescription([FromRoute] Guid id, CourseUpdateRequest course)
  {
    await _courseService.UpdateDescription(id, course.Text);
    return Ok();
  }

  [HttpPatch("update/course/categories/{id}")]
  public async Task<ActionResult> UpdateCategories([FromRoute] Guid id,
    CourseCategoriesUpdateRequest course)
  {
    await _courseService.UpdateCategories(id, course.Categories);
    return Ok();
  }
}