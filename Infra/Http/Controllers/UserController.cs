using API.Application.DTOs;
using API.Application.DTOs.User;
using API.Application.Services;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Infra.Http.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/users")]
[ApiController]
public class UserController(UserService userService, AuthService authService) : BaseController
{
    [Authorize]
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await userService.GetAllUsers();
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
