using ErrorOr;
using SampleProject.Application.Tasks.Common;
using SampleProject.Domain.Enums;

namespace SampleProject.Application.Tasks.Queries.GetTaskByStatusOrUserId;
public record GetTaskByStatusOrUserIdQuery(Status status, string? userId) : IRequest<ErrorOr<List<TaskDto>>>;
