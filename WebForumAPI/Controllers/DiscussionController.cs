using System.Security.Claims;
using Application.Services;
using Core.Entities;
using Core.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebForumAPI.Controllers;


[ApiController]
[Route("api")]
public class DiscussionController : ControllerBase
{
  private readonly DiscussionService _service;

  public DiscussionController(DiscussionService service)
  {
    _service = service;
  }

  [HttpGet("discussions")]
  public async Task<ActionResult<List<Discussion>>> Get()
  {
    var discussions = await _service.Get();
    if (discussions.Count == 0)
    {
      return NotFound("Not found discussions");
    }

    return Ok(discussions);
  }
  
  [HttpGet ("discussions/{id}")]
  public async Task<ActionResult<List<Discussion>>> GetById([FromRoute] Guid id)
  {
    var discussion = await _service.GetById(id);
    if (!ModelState.IsValid)
    {
      return BadRequest();
    }

    return Ok(discussion);
  }
  
  [HttpGet ("discussions/by-title/{title}")]
  public async Task<ActionResult<List<Discussion>>> GetByTitle([FromRoute] string title)
  {
    var discussion = await _service.GetByTitle(title);
    if (!ModelState.IsValid)
    {
      return BadRequest();
    }

    return Ok(discussion);
  }

  [Authorize]
  [HttpPost("add/discussion")]
  public async Task<ActionResult> Add([FromBody] DiscussionRequest discussionRequest)
  {
    await _service.Add(discussionRequest.Title, discussionRequest.Content, discussionRequest.Creator, discussionRequest.Categories);
    return Ok("Discussion created");
  }
  
  [Authorize]
  [HttpPatch("update/title/discussion")]
  public async Task<ActionResult> UpdateTitle([FromBody] DiscussionUpdateTitleRequest discussionRequest)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var discussion = await _service.GetById(discussionRequest.id);
    var role = User.FindFirst(ClaimTypes.Role)?.Value;
    if (role == "admin" || userId == discussion.Creator.Id.ToString())
    {
      await _service.UpdateTitle(discussionRequest.id, discussionRequest.Title);
      return Ok();
    }

    return Forbid();
  }
  
  [Authorize]
  [HttpPatch("update/content/discussion")]
  public async Task<ActionResult> UpdateContent([FromBody] DiscussionUpdateContentRequest discussionRequest)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var discussion = await _service.GetById(discussionRequest.id);
    var role = User.FindFirst(ClaimTypes.Role)?.Value;
    if (role == "admin" || userId == discussion.Creator.Id.ToString())
    {
      await _service.UpdateContent(discussionRequest.id, discussionRequest.Content);
      return Ok();
    }

    return Forbid();
  }

  [Authorize]
  [HttpDelete("delete/{id}")]
  public async Task<ActionResult> Delete(Guid id)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var discussion = await _service.GetById(id);
    var role = User.FindFirst(ClaimTypes.Role)?.Value;
    if (role == "admin" || userId == discussion.Creator.Id.ToString())
    {
      await _service.Delete(id);
      return Ok($"Discussion with {id}, deleted");
    }

    return Forbid();
  }
}