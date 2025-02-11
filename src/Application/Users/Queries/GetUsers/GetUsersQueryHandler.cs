using ErrorOr;
using Microsoft.AspNetCore.Identity;
using SampleProject.Application.Authentication.Common;
using SampleProject.Domain.Entities;
namespace SampleProject.Application.Users.Queries.GetUsers;
public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ErrorOr<List<UserDto>>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<ErrorOr<List<UserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userManager.Users.AsNoTracking()
                                       .Where(u => request.userId != null ? u.Id == request.userId : true)
                                       .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                                       .ToListAsync();

        return users;

    }
}
