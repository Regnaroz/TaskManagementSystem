using ErrorOr;
using SampleProject.Application.Tasks.Common;

namespace SampleProject.Application.Tasks.Queries.GetTasksDueToday;
public record GetTasksDueTodayQuery() : IRequest<ErrorOr<List<TaskDto>>>;
