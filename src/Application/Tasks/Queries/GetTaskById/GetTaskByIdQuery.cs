using ErrorOr;
using SampleProject.Application.Tasks.Common;

namespace SampleProject.Application.Tasks.Queries.GetTaskById;
public record GetTaskByIdQuery(int Id) : IRequest<ErrorOr<TaskDto>>;
