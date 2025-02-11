using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using SampleProject.Application.Authentication.Login.Commands.LoginUser;
using SampleProject.Domain.Entities;

namespace SampleProject.Application.Users.Commands.UpdateUser;
public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UpdateUserCommandValidator(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;

        RuleFor(v => v.updateUserDto.Email).NotEmpty().MinimumLength(3).MaximumLength(64).EmailAddress();
        RuleFor(v => v.updateUserDto.FirstName).NotEmpty().MinimumLength(3).MaximumLength(64);
        RuleFor(v => v.updateUserDto.LastName).NotEmpty().MinimumLength(3).MaximumLength(64);
        RuleFor(v => v.updateUserDto.userId).NotEmpty().MustAsync(UserExists).WithMessage("User does not exist.");
    }

    private async Task<bool> UserExists(string userId, CancellationToken token)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user != null;
    }
}
