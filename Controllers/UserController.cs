using Microsoft.AspNetCore.Mvc;
using FilmProject.DTO;
using FilmProject.Services.Abstract;

namespace FilmProject.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] DtoSignUp signUpDto)
    {
        var result = await _userService.SingUpAsync(signUpDto);

        if (result == "User created successfully!")
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] DtoSignIn signInDto)
    {
        var result = await _userService.SignInAsync(signInDto);

        if (result == "User sign in successfully")
        {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] DtoUpdateUser dtoUpdateUser)
    {
        // if (!ModelState.IsValid)
        // {
        //     return BadRequest(ModelState);
        // }

        var result = await _userService.UpdateUserAsync(id, dtoUpdateUser);
        if (result == "User not found!")
        {
            return NotFound(result);
        }

        return Ok(result);
    }
}