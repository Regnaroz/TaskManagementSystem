using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using SampleProject.Application.Authentication.Common;
using SampleProject.Application.Common.Interfaces;
using SampleProject.Domain.Constants;
using SampleProject.Domain.Entities;
using SampleProject.Domain.Errors;
using static SampleProject.Application.Authentication.Common.AuthenticationResult;

namespace SampleProject.Application.Authentication.Register.Commands.RegisterUser;
public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ErrorOr<string>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    async Task<ErrorOr<string>> IRequestHandler<RegisterUserCommand, ErrorOr<string>>.Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var isEmailExist = await _userManager.FindByEmailAsync(request.email);
        if (isEmailExist != null)
        {
            return Errors.Authentication.DuplicateEmail;
        }

        var user = new ApplicationUser()
        {
            Email = request.email,
            UserName = request.email,
            FirstName = request.firstName,
            LastName = request.lastName,
        };

        var result = await _userManager.CreateAsync(user, request.password);
        if (!result.Succeeded)
        {
            return HandleRegiserationErrors(result);
        }

        user = await _userManager.FindByEmailAsync(request.email);
        if (user != null)
        {
            await _userManager.AddToRoleAsync(user!, Roles.User);
            return user.Id;
        }
        else
        {
            return Errors.Authentication.UserNotFound;
        }
    }

    private ErrorOr<string> HandleRegiserationErrors(IdentityResult result)
    {
        var errors = new List<Error>();
        foreach (var error in result.Errors)
        {
            errors.Add(Error.Failure(code : error.Code , description: error.Description));
        }
        
        return errors;
    }
}
