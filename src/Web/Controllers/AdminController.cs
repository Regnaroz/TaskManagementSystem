using ErrorOr;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Application.Admin.Queries.GetTasksPerUser;
using SampleProject.Application.Common.Models;
using SampleProject.Application.Tasks.Common;
using SampleProject.Application.Tasks.Queries.GetTasks;
using SampleProject.Application.Tasks.Queries.GetTasksDueToday;
using SampleProject.Domain.Constants;
using SampleProject.Domain.StoredProcedureObjects;

namespace SampleProject.Web.Controllers;
[Route("api/[controller]")]
[Authorize(Roles = Roles.Administrator)]
[ApiController]
public class AdminController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public AdminController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet("GetTasksCountByUser")]
    public async Task<ActionResult<ErrorOr<List<UserTaskCountDto>>>> GetTasksCountByUser()
    {
        var command = new GetTasksCountByUserQuery();

        var result = await _sender.Send(command);

        return result.IsError ? Problem(result.Errors) : Ok(result.Value);
    }

    [HttpGet("GetTasksDueToday")]
    public async Task<ActionResult<ErrorOr<List<TaskDto>>>> GetTasksDueToday()
    {
        var command = new GetTasksDueTodayQuery();

        var result = await _sender.Send(command);

        return result.IsError ? Problem(result.Errors) : Ok(result.Value);
    }

}
