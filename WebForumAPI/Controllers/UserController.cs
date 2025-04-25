using System.Security.Claims;
using Application.Services;
using Core.Requests;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace WebForumAPI.Controllers;

[ApiController]
[Route("api")]
public class UserController : ControllerBase
{
  private readonly UserService _userService;
  public UserController(UserService userService)
  {
    _userService = userService;
  }
  
  [HttpGet("users")]
  public async Task<ActionResult<User>> Get()
  {
    var users = await _userService.Get();
    return Ok(users);
  }

  [HttpPost("register")]
  public async Task<ActionResult> Register([FromBody] UserRequest user)
  {
    await _userService.Register(user.Username, user.Password, user.Role);
    return Ok("User created successfully!");
  }

  [HttpPost("login")]
  public async Task<ActionResult<string>> Login([FromBody] UserRequestLogin user)
  {
    var token = await _userService.Login(user.Username, user.Password);
    if (token == "Incorrect password") return BadRequest(token);
    HttpContext.Response.Cookies.Append("YUMI", token);
    return Ok();
  }

  [HttpGet("users/id/{id}")]
  public async Task<ActionResult<User>> GetById([FromRoute] Guid id)
  {
    var user = await _userService.GetById(id);
    return Ok(user);
  }
  
  [HttpGet("users/username/{username}")]
  public async Task<ActionResult<User>> GetByUsername([FromRoute] string username)
  {
    var user = await _userService.GetByUsername(username);
    return Ok(user);
  }

  [Authorize]
  [HttpPatch("change/username")]
  public async Task<ActionResult> UpdateUsername([FromBody] UsernameRequest usernameRequest)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var user = await _userService.GetById(usernameRequest.Id);
    var role = User.FindFirst(ClaimTypes.Role)?.Value;
    if (role == "admin" || userId == user.Id.ToString())
    {
      await _userService.UpdateUsername(usernameRequest.Id, usernameRequest.Username);
      return Ok($"New username {usernameRequest.Username}"); 
    }
    return Forbid();
  }

  [Authorize]
  [HttpPatch("change/password")]
  public async Task<ActionResult> UpdatePassword([FromBody] PasswordRequest passwordRequest)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var user = await _userService.GetById(passwordRequest.Id);
    if (userId == user.Id.ToString())
    {
      await _userService.UpdatePassword(passwordRequest.Id, passwordRequest.Password);
      return Ok($"New password!"); 
    }
    return Forbid();
  }
  
  [Authorize]
  [HttpDelete("delete/user")]
  public async Task<ActionResult> Delete(Guid id)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var role = User.FindFirst(ClaimTypes.Role)?.Value;
    var user = await _userService.GetById(id);
    if (role == "admin" || userId == user.Id.ToString())
    {
      await _userService.Delete(id);
      return Ok("Delete successfully");
    }
    return Forbid();
  }

  [Authorize]
  [HttpPost("register/user-on-course")]
  public async Task<ActionResult> RegisterOnCourse(
    [FromBody] UserRegisterOnCourseRequest userRegisterOnCourseRequest)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var user = await _userService.GetById(userRegisterOnCourseRequest.UserId);
    var role = User.FindFirst(ClaimTypes.Role)?.Value;
    if (role == "admin" || userId == user.Id.ToString())
    {
      await _userService.RegisterOnCourse(userRegisterOnCourseRequest.CourseId,
        userRegisterOnCourseRequest.UserId);
      return Ok(); 
    }
    return Forbid();
  }
}