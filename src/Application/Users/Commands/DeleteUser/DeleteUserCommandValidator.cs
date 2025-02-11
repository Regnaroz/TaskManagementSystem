using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using SampleProject.Application.Authentication.Login.Commands.LoginUser;
using SampleProject.Domain.Entities;

namespace SampleProject.Application.Users.Commands.DeleteUser;
public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public DeleteUserCommandValidator(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        RuleFor(v => v.userId).MustAsync(UserExists).WithMessage("User does not exist.");
    }

    private async Task<bool> UserExists(string userId, CancellationToken token)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user != null;
    }
}
