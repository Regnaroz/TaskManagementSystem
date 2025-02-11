using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleProject.Application.Authentication.Register.Commands.RegisterUser;

namespace SampleProject.Application.Authentication.Login.Commands.LoginUser;
public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(v => v.email).NotEmpty().MinimumLength(3).MaximumLength(64).EmailAddress();
        RuleFor(v => v.password).NotEmpty().MinimumLength(3).MaximumLength(64);
    }
}
