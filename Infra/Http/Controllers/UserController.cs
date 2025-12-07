using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using WebApi.Application.DTOs.User;
using WebApi.Application.Services;

namespace WebApi.Infra.Http.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/users")]
[ApiController]
public class UserController(UserService userService, AuthService authService) : BaseController
{
    [EnableRateLimiting("FixedWindow")]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, int pageSize = 20)
    {
        var users = await userService.GetAllUsersWithPagingOptions(page, pageSize);
        return Ok(users);
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await userService.GetUserById(id);
        if (user is null) return NotFound();

        return Ok(user);
    }

    [HttpPost]
    [Route("seed")]
    public async Task<IActionResult> SeedUsers()
    {

        var users = Enumerable.Range(101, 200).Select(idx => new CreateUserDTO($"User {idx}", $"user{idx}@example.com")).ToList();
        
        foreach(var user in users)
        {
            await userService.AddUser(user);
        }

        return Ok();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDTO userDto)
    {
        await userService.AddUser(userDto);
        return CreatedResult("Usuário criado com sucesso");
    }

    [Authorize(Policy = "CanUpdateUser")]
    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, UpdateUserDTO user)
    {
        await userService.UpdateUser(id, user);
        return UpdatedResult("Usuário atualizado com sucesso");
    }

    [Authorize]
    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> Remove([FromRoute] Guid id)
    {
        await userService.RemoveUser(id);
        return OkResult("Usuário removido com sucesso");
    }  
    
    [HttpGet("{id:guid}/token")]
    public async Task<IActionResult> GetTokenByUserId(Guid id)
    {
        var user = await userService.GetUserById(id);
        if (user is null) return NotFound();

        var token = authService.GenerateToken(new Domain.Entities.User(user.Name, user.Email));
        if (token is null) return NotFound();

        return Ok(new { Token = token });
    }
}
