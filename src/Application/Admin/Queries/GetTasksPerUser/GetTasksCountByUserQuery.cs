using ErrorOr;
using SampleProject.Domain.StoredProcedureObjects;

namespace SampleProject.Application.Admin.Queries.GetTasksPerUser;
public record GetTasksCountByUserQuery() : IRequest<ErrorOr<List<UserTaskCountDto>>>;
