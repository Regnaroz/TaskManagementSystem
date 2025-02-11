using SampleProject.Domain.Entities;

namespace SampleProject.Application.Authentication.Common;
public record UserDto(string Id , string firstName , string lastName , string email)
{
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ApplicationUser, UserDto>();
        }
    }
}
