using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using SampleProject.Application.Tasks.Common;

namespace SampleProject.Application.Tasks.Commands.AddTask;
public record AddTaskCommand(TaskDto taskDto): IRequest<ErrorOr<int>>;

