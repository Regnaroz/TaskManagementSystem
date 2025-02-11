using SampleProject.Application.Authentication.Common;
using SampleProject.Domain.Entities;
using SampleProject.Domain.Enums;

namespace SampleProject.Application.Tasks.Common;
public class TaskDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public Status Status { get; set; }
    public PriorityLevel Priority { get; set; }
    public DateTime DueDate { get; set; }
    public required string UserId { get; set; }
    public  UserDto? User { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UserTask, TaskDto>();
        }
    }

}
