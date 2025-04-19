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
  public async Task<ActionResult<List<Discussion>>> GetById(Guid id)
  {
    var discussion = await _service.GetById(id);
    if (!ModelState.IsValid)
    {
      return BadRequest();
    }

    return Ok(discussion);
  }
  
  [HttpGet ("discussions/by-title/{title}")]
  public async Task<ActionResult<List<Discussion>>> GetByTitle(string title)
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
    await _service.UpdateTitle(discussionRequest.id, discussionRequest.Title);
    return Ok($"Discussion updated, new title {discussionRequest.Title}");
  }
  
  [Authorize]
  [HttpPatch("update/content/discussion")]
  public async Task<ActionResult> UpdateContent([FromBody] DiscussionUpdateContentRequest discussionRequest)
  {
    await _service.UpdateContent(discussionRequest.id, discussionRequest.Content);
    return Ok($"Discussion updated, new content {discussionRequest.Content}");
  }

  [Authorize(Roles = "admin")]
  [HttpDelete("delete/{id}")]
  public async Task<ActionResult> Delete(Guid id)
  {
    await _service.Delete(id);
    return Ok($"Discussion with {id}, deleted");
  }
}