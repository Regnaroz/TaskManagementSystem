using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using SampleProject.Application.Authentication.Common;
using SampleProject.Application.Common.Interfaces;
using SampleProject.Domain.Entities;
using SampleProject.Domain.Errors;

namespace SampleProject.Application.Authentication.Login.Commands.LoginUser;
public class LoginCommandHandler : IRequestHandler<LoginUserCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtGenerator;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public LoginCommandHandler(IJwtTokenGenerator jwtGenerator, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _jwtGenerator = jwtGenerator;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.email);
        if (user == null)
        {
            return Errors.Authentication.InvalidCredentials;
        }
        var result = await _signInManager.PasswordSignInAsync(user, request.password, false, lockoutOnFailure: false);
        if (!result.Succeeded)
        {
            return Errors.Authentication.InvalidCredentials ;
        }
        var token = _jwtGenerator.GenerateToken(user);
        var userDto = new UserDto(user.Id, user.FirstName, user.LastName, user.Email!);
        return new AuthenticationResult(user : userDto, token : token);   
    }
}
