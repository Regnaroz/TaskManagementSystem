using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using SampleProject.Application.Authentication.Register.Commands.RegisterUser;
using SampleProject.Application.Common.Interfaces;
using SampleProject.Application.Tasks.Commands.AddTask;
using SampleProject.Application.Tasks.Common;
using SampleProject.Domain.Constants;
using SampleProject.Domain.Entities;
using SampleProject.Domain.Enums;

namespace SampleProject.Application.Tasks.Commands.UpdateTask;
public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public UpdateTaskCommandValidator(UserManager<ApplicationUser> userManager, IUser user, IApplicationDbContext context)
    {
        _userManager = userManager;
        _user = user;
        _context = context;

        RuleFor(v => v.taskDto.Id).NotEmpty().MustAsync(TaskExists).WithMessage("Task is not found");
        RuleFor(v => v.taskDto.Title).NotEmpty().MinimumLength(3).MaximumLength(64);
        RuleFor(v => v.taskDto.Description).NotEmpty().MinimumLength(3).MaximumLength(265);
        RuleFor(v => (int)v.taskDto.Status).NotEqual(0).InclusiveBetween((int)Status.NotStarted, (int)Status.Deleted);
        RuleFor(v => (int)v.taskDto.Priority).NotEqual(0).InclusiveBetween((int)PriorityLevel.Low, (int)PriorityLevel.High);
        RuleFor(v => v.taskDto.DueDate.Date).NotNull().GreaterThan(DateTime.UtcNow);
        RuleFor(v => v.taskDto.UserId).NotNull().MustAsync(UserExists).WithMessage("User does not exist.");
        if (_user.Role != Roles.Administrator)
        {
            RuleFor(v => v.taskDto).MustAsync(AssignedUserTask).WithMessage("User isnt Assigned to the task");
        }
       
    }

    private async Task<bool> TaskExists(int taskId, CancellationToken token)
    {
        var task = await _context.Tasks.FindAsync(taskId);
        return task != null;
    }

    private async Task<bool> AssignedUserTask(TaskDto dto, CancellationToken token)
    {
        var taskUserAssignment = await _context.Tasks.FindAsync(dto.Id);
        if(taskUserAssignment != null && taskUserAssignment.UserId ==  _user.Id)
        {
            return true;
        }
        return false;
    }
    private async Task<bool> UserExists(string userId, CancellationToken token)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user != null;
    }
}

