using Application.Services;
using Core.Entities;
using Core.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebForumAPI.Controllers;

[ApiController]
[Route ("api")]
public class LessonController : ControllerBase
{
  private readonly LessonService _lessonService;

  public LessonController(LessonService lessonService)
  {
    _lessonService = lessonService;
  }
  [HttpPost("add/lesson")]
  public async Task<ActionResult> Add([FromBody] LessonRequest lessonRequest)
  {
    await _lessonService.Add(lessonRequest.Title, lessonRequest.Minutes, lessonRequest.CourseId);
    return Ok();
  }

  [HttpGet("lessons/{id}")]
  public async Task<ActionResult<Lesson>> GetById([FromRoute] Guid id)
  {
    var lesson = await _lessonService.GetById(id);
    if (lesson == null)
    {
      return NotFound();
    }
    return Ok(lesson);
  }

  [HttpGet("lessons/by-title")]
  public async Task<ActionResult<Lesson>> GetByTitle([FromBody] string title)
  {
    var lesson = await _lessonService.GetByTitle(title);
    if (lesson == null) return NotFound();
    return Ok(lesson);
  }

  [HttpDelete("delete/lesson/{id}")]
  public async Task<ActionResult> Delete([FromRoute] Guid id)
  {
    await _lessonService.Delete(id);
    return Ok();
  }
}