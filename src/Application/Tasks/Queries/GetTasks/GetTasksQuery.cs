using ErrorOr;
using SampleProject.Application.Common.Models;
using SampleProject.Application.Tasks.Common;

namespace SampleProject.Application.Tasks.Queries.GetTasks;
public record GetTasksQuery(TasksFilter filter,bool isAdmin) : IRequest<ErrorOr<PaginatedList<TaskDto>>>;
