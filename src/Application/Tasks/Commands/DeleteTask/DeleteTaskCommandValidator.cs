using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SampleProject.Application.Authentication.Login.Commands.LoginUser;
using SampleProject.Application.Common.Interfaces;
using SampleProject.Application.Tasks.Common;
using SampleProject.Domain.Constants;
using SampleProject.Domain.Entities;

namespace SampleProject.Application.Tasks.Commands.DeleteTask;
public class DeleteTaskCommandValidator : AbstractValidator<DeleteTaskCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;
    public DeleteTaskCommandValidator(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;

        RuleFor(v => v.taskId).MustAsync(TaskExists).WithMessage("Task isnt found");
        if (_user.Role != Roles.Administrator)
        {
            RuleFor(v => v.taskId).MustAsync(AssignedUserTask).WithMessage("User isnt Assigned to the task");
        }
    }
    private async Task<bool> TaskExists(int taskId, CancellationToken token)
    {
        var task = await _context.Tasks.FindAsync(taskId);
        return task != null;
    }
    private async Task<bool> AssignedUserTask(int taskId, CancellationToken token)
    {
        var taskUserAssignment = await _context.Tasks.FindAsync(taskId);
        if (taskUserAssignment != null && taskUserAssignment.UserId == _user.Id)
        {
            return true;
        }
        return false;
    }
}
