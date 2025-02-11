using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Application.Authentication.Common;
using SampleProject.Application.Authentication.Register.Commands.RegisterUser;
using SampleProject.Application.Users.Commands.DeleteUser;
using SampleProject.Application.Users.Commands.UpdateUser;
using SampleProject.Application.Users.Queries.GetUsers;
using SampleProject.Domain.Constants;

namespace SampleProject.Web.Controllers;
[Route("api/[controller]")]
[Authorize(Roles = Roles.Administrator)]
[ApiController]
public class UserController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public UserController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }
    [HttpPost("AddUser")]
    public async Task<ActionResult<string>> AddUser(RegisterUserDto userDto)
    {
        var command = _mapper.Map<RegisterUserCommand>(userDto);

        var result = await _sender.Send(command);

        return result.IsError ? Problem(result.Errors) : Ok(result.Value);
    }

    [HttpPut("UpdateUser")]
    public async Task<ActionResult<bool>> UpdateUser(UpdateUserDto updateUserDto)
    {
        var command = new UpdateUserCommand(updateUserDto);

        var result = await _sender.Send(command);

        return result.IsError ? Problem(result.Errors) : Ok(result.Value);
    }
    [HttpDelete("DeleteUser")]
    public async Task<ActionResult<bool>> UpdateUser(string userId)
    {
        var command = new DeleteUserCommand(userId);

        var result = await _sender.Send(command);

        return result.IsError ? Problem(result.Errors) : Ok(result.Value);
    }

    [HttpGet("GetUsers/{userId?}")]
    public async Task<ActionResult<bool>> GetUsers(string? userId = null)
    {
        var command = new GetUsersQuery(userId);

        var result = await _sender.Send(command);

        return result.IsError ? Problem(result.Errors) : Ok(result.Value);
    }
}
