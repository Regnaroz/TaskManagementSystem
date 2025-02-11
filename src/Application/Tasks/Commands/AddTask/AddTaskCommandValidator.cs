using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SampleProject.Application.Authentication.Register.Commands.RegisterUser;
using SampleProject.Domain.Entities;
using SampleProject.Domain.Enums;

namespace SampleProject.Application.Tasks.Commands.AddTask;
public class AddTaskCommandValidator: AbstractValidator<AddTaskCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AddTaskCommandValidator(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        RuleFor(v => v.taskDto.Title).NotEmpty().MinimumLength(3).MaximumLength(64);
        RuleFor(v => v.taskDto.Description).NotEmpty().MinimumLength(3).MaximumLength(265);
        RuleFor(v => (int)v.taskDto.Status).NotEqual(0).InclusiveBetween((int)Status.NotStarted,(int)Status.Deleted);
        RuleFor(v => (int)v.taskDto.Priority).NotEqual(0).InclusiveBetween((int)PriorityLevel.Low,(int)PriorityLevel.High);
        RuleFor(v => v.taskDto.DueDate.Date).NotNull().GreaterThan(DateTime.UtcNow);
        RuleFor(v => v.taskDto.UserId).NotNull().MustAsync(UserExists).WithMessage("User does not exist.");
    }
    private async Task<bool> UserExists(string userId, CancellationToken token)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user != null;
    }
}

