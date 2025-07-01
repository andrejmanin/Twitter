using Database.DTO;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Server.Services.Interfaces;

namespace Server.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService) => _userService = userService;

    [HttpPost("createUser")]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDto userDto)
    {
        string response = await _userService.CreateUserAsync(userDto);
        if(response != "User was created") return BadRequest(response);
        return Ok(response);
    }

    [HttpGet("checkUser")]
    public async Task<IActionResult> CheckUser(string email, string password)
    {
        return Ok(await _userService.CheckUser(email, password));
    }
    
    [HttpGet("getUsers")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetUsersAsync();
        return Ok(users);
    }

    [HttpGet("getUser/{id:guid}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var user = await _userService.GetUserAsync(id);
        if(user is null) return NotFound();
        return Ok(user);
    }
    
    [HttpGet("getUser/{email}")]
    public async Task<IActionResult> GetUser(string email)
    {
        var user =  await _userService.GetUserAsync(email);
        
        if(user is null) return NotFound();
        return Ok(user);
    }

    [HttpGet("getUserByNickname/{nickname}")]
    public async Task<IActionResult> GetUserByNickname(string nickname)
    {
        var user =  await _userService.GetUserByNicknameAsync(nickname);
        if(user is null) return NotFound();
        return Ok(user);
    }

    [HttpPut("updateUser")]
    public async Task<IActionResult> UpdateUserAsync(UpdateUserDto userDto)
    {
        string response = await _userService.UpdateUserAsync(userDto);
        if(response != "User was updated") return BadRequest(response);
        return Ok(response);
    }

    [HttpDelete("deleteUser/{id:guid}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        if(!await _userService.DeleteUserAsync(id)) return BadRequest("User was not deleted or was already deleted");
        return Ok("User was deleted");
    }

    [HttpDelete("deleteUser/{email}")]
    public async Task<IActionResult> DeleteUser(string email)
    {
        if(!await _userService.DeleteUserAsync(email)) return BadRequest("User was not deleted or was already deleted");
        return Ok("User was deleted");
    }
}