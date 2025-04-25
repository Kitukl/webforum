using System.Security.Claims;
using Application.Services;
using Core.Entities;
using Core.Requests;
using Microsoft.AspNetCore.Authorization;
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
  [Authorize (Roles = "admin, lecture")]
  [HttpPost("add/lesson")]
  public async Task<ActionResult> Add([FromBody] LessonRequest lessonRequest)
  {
    await _lessonService.Add(lessonRequest.Title, lessonRequest.Minutes, lessonRequest.CourseId);
    return Ok();
  }

  [Authorize]
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

  [Authorize]
  [HttpGet("lessons/by-title")]
  public async Task<ActionResult<Lesson>> GetByTitle([FromBody] string title)
  {
    var lesson = await _lessonService.GetByTitle(title);
    if (lesson == null) return NotFound();
    return Ok(lesson);
  }

  [Authorize]
  [HttpDelete("delete/lesson/{id}")]
  public async Task<ActionResult> Delete([FromRoute] Guid id)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var role = User.FindFirst(ClaimTypes.Role)?.Value;
    var lesson = await _lessonService.GetById(id);
    if (role == "admin" || userId == lesson.Course.Creator.Id.ToString())
    {
      await _lessonService.Delete(id);
      return Ok();
    }
    return Forbid();
  }
}