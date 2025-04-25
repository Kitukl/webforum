using System.Security.Claims;
using Application.Services;
using Core.Entities;
using Core.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebForumAPI.Controllers;


[ApiController]
[Route("api")]
public class CommentController : ControllerBase
{
  private readonly CommentService _commentService;

  public CommentController(CommentService commentService)
  {
    _commentService = commentService;
  }
  
  [Authorize]
  [HttpPost("add/comment/{id}")]
  public async Task<ActionResult> Add([FromBody] CommentRequest commentRequest, [FromRoute] Guid id)
  {
    await _commentService.Add(commentRequest.Creator, id, commentRequest.Content);
    return Ok("Comment created");
  }

  [HttpGet("comments")]
  public async Task<ActionResult<List<Comment>>> Get()
  {
    return Ok(await _commentService.Get());
  }

  [HttpGet("comments/{id}")]
  public async Task<ActionResult<Comment>> GetById([FromRoute]Guid id)
  {
    return Ok(await _commentService.GetById(id));
  }

  [HttpGet("comments/by-title")]
  public async Task<ActionResult<Comment>> GetByTitle(string title)
  {
    return Ok(await _commentService.GetByTitle(title));
  }

  [HttpGet("commets/by-user")]
  public async Task<ActionResult<List<Comment>>> GetByUser(Guid id)
  {
    return Ok(await _commentService.GetByUser(id));
  }

  [Authorize]
  [HttpDelete("delete/comment/id")]
  public async Task<ActionResult> Delete(Guid id)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var comment = await _commentService.GetById(id);
    var role = User.FindFirst(ClaimTypes.Role)?.Value;
    if (role == "admin" || userId == comment.Creator.Id.ToString())
    {
      await _commentService.Delete(id);
      return Ok();
    }

    return Forbid();
  }

  [Authorize]
  [HttpPatch]
  public async Task<ActionResult> Update([FromBody] Comment com)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var comment = await _commentService.GetById(com.Id);
    var role = User.FindFirst(ClaimTypes.Role)?.Value;
    if (role == "admin" || userId == comment.Creator.Id.ToString())
    {
      await _commentService.Update(com.Id, com.Content);
      return Ok();
    }

    return Forbid();
  }
}