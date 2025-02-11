using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using SampleProject.Application.Authentication.Common;

namespace SampleProject.Application.Tasks.Commands.DeleteTask;
public record DeleteTaskCommand(int taskId,bool isAdmin) : IRequest<ErrorOr<bool>>;
