using ErrorOr;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Application.Authentication.Register.Commands.RegisterUser;
using SampleProject.Application.Common.Interfaces;
using SampleProject.Application.Common.Models;
using SampleProject.Application.Tasks.Commands.AddTask;
using SampleProject.Application.Tasks.Commands.DeleteTask;
using SampleProject.Application.Tasks.Commands.UpdateTask;
using SampleProject.Application.Tasks.Common;
using SampleProject.Application.Tasks.Queries.GetTaskById;
using SampleProject.Application.Tasks.Queries.GetTaskByStatusOrUserId;
using SampleProject.Application.Tasks.Queries.GetTasks;
using SampleProject.Application.Tasks.Queries.GetTasksDueToday;
using SampleProject.Domain.Constants;
using SampleProject.Domain.Enums;

namespace SampleProject.Web.Controllers;
[Route("api/[controller]")]
[Authorize]
[ApiController]
public class TaskController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    private readonly IUser _user;
    public TaskController(ISender sender, IMapper mapper, IUser user)
    {
        _sender = sender;
        _mapper = mapper;
        _user = user;
    }

    [HttpPost("AddTask")]
    public async Task<ActionResult<int>> AddTask(TaskDto taskDto)
    {
        var command = new AddTaskCommand(taskDto);

        var result = await _sender.Send(command);

        return result.IsError ? Problem(result.Errors) : Ok(result.Value);
    }

    [HttpPut("UpdateTask")]
    public async Task<ActionResult<bool>> UpdateTask(TaskDto taskDto)
    {
        bool isAdmin = false;
        if (_user.Role == Roles.Administrator)
        {
            isAdmin = true;
        }
        var command = new UpdateTaskCommand(taskDto, isAdmin);

        var result = await _sender.Send(command);

        return result.IsError ? Problem(result.Errors) : Ok(result.Value);
    }

    [HttpDelete("DeleteTask/{taskId}")]
    public async Task<ActionResult<bool>> DeleteTask(int taskId)
    {
        bool isAdmin = false;
        if (_user.Role == Roles.Administrator)
        {
            isAdmin = true;
        }
        var command = new DeleteTaskCommand(taskId, isAdmin);

        var result = await _sender.Send(command);

        return result.IsError ? Problem(result.Errors) : Ok(result.Value);
    }


    [HttpGet("GetTasks")]
    public async Task<ActionResult<ErrorOr<PaginatedList<TaskDto>>>> GetTasks(TasksFilter filter)
    {
        bool isAdmin = false;
        if (_user.Role == Roles.Administrator)
        {
            isAdmin = true;
        }
        var command = new GetTasksQuery(filter, isAdmin);

        var result = await _sender.Send(command);

        return result.IsError ? Problem(result.Errors) : Ok(result.Value);
    }

    [HttpGet("GetTaskById/{Id}")]
    public async Task<ActionResult<ErrorOr<PaginatedList<TaskDto>>>> GetTaskById(int id)
    {
        var command = new GetTaskByIdQuery(id);

        var result = await _sender.Send(command);

        return result.IsError ? Problem(result.Errors) : Ok(result.Value);
    }

    [HttpGet("GetTaskByStatusAndUserId/{status}/{userId?}")]
    public async Task<ActionResult<ErrorOr<List<TaskDto>>>> GetTaskByStatusAndUserId(Status status,string? userId)
    {
        var command = new GetTaskByStatusOrUserIdQuery(status,userId);

        var result = await _sender.Send(command);

        return result.IsError ? Problem(result.Errors) : Ok(result.Value);
    }
}
