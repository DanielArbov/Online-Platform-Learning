using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Talent;
[ApiController]
public class UsersController : ControllerBase,IDisposable
{

    private readonly UserService _userService;
    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    // POST request to register a new user
    [HttpPost("api/register")]
    public IActionResult Register([FromBody] User user)
    {
        if (!ModelState.IsValid) return BadRequest(new ValidationError(ModelState.GetAllErrors()));
        if (_userService.IsEmailTaken(user.Email)) return BadRequest(new ValidationError("email is already exists"));
        string token = _userService.Register(user);
        Console.WriteLine(user.Name);
        Console.WriteLine(token);

        return Created("", token);
    }


    // POST request to log in an existing user
    [HttpPost("api/login")]
    public IActionResult LogIn([FromBody] Credentials credentials)
    {
        string? token = _userService.LogIn(credentials);
        if (token == null) return Unauthorized(new UnauthorizedError("Incorrect email or  password"));
        return Ok(token);
    }



    // GET request to retrieve all users (accessible by authorized users)
    [HttpGet("api/users")]
    public IActionResult GetAllUsers()
    {
        return Ok(_userService.GetAllUsers());
    }


    // Disposes of the user service to free up resources
    public void Dispose()
    {
        _userService.Dispose();
    }


}

