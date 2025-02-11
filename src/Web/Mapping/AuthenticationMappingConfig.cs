using Mapster;
using Microsoft.AspNetCore.Identity.Data;
using SampleProject.Application.Authentication.Common;
using SampleProject.Application.Authentication.Login.Commands.LoginUser;
using SampleProject.Application.Authentication.Register.Commands.RegisterUser;

namespace SampleProject.Web.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterUserDto, RegisterUserCommand>();
        config.NewConfig<LoginUserDto, LoginUserCommand>();

        config.NewConfig<AuthenticationResult, AuthenticationResponseDto>()
            .Map(dest => dest.token, src => src.token)
            .Map(dest => dest.Id, src => src.user.Id)
            .Map(dest => dest.email, src => src.user.email);
            
        // by this we mean go check if the unassigned properties can be assigned from this object
    }
}
