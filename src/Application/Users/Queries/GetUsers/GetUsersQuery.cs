using ErrorOr;
using SampleProject.Application.Authentication.Common;

namespace SampleProject.Application.Users.Queries.GetUsers;
public record GetUsersQuery(string? userId = null) : IRequest<ErrorOr<List<UserDto>>>;
