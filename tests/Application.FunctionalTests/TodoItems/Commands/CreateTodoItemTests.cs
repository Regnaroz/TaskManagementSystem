using SampleProject.Application.Common.Exceptions;
using SampleProject.Application.Tasks.Commands.AddTask;
using SampleProject.Application.Tasks.Common;
using SampleProject.Domain.Entities;

namespace SampleProject.Application.FunctionalTests.TodoItems.Commands;

using static Testing;

public class CreateTodoItemTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var taskDto = new TaskDto()
        {
            Title = "to",
            Description = "de",
            UserId = string.Empty,
        };
        var command = new AddTaskCommand(taskDto);

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    //[Test]
    //public async Task ShouldCreateTodoItem()
    //{
    //    var userId = await RunAsDefaultUserAsync();

    //    var listId = await SendAsync(new CreateTodoListCommand
    //    {
    //        Title = "New List"
    //    });

    //    var command = new CreateTodoItemCommand
    //    {
    //        ListId = listId,
    //        Title = "Tasks"
    //    };

    //    var itemId = await SendAsync(command);

    //    var item = await FindAsync<TodoItem>(itemId);

    //    item.Should().NotBeNull();
    //    item!.ListId.Should().Be(command.ListId);
    //    item.Title.Should().Be(command.Title);
    //    item.CreatedBy.Should().Be(userId);
    //    item.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    //    item.LastModifiedBy.Should().Be(userId);
    //    item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    //}
}
