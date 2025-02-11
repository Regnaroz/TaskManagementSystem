using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using SampleProject.Application.Tasks.Common;

namespace SampleProject.Application.Tasks.Commands.UpdateTask;
public record UpdateTaskCommand(TaskDto taskDto,bool isAdmin) : IRequest<ErrorOr<bool>>;

