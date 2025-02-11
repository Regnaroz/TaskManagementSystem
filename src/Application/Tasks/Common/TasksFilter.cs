using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleProject.Domain.Enums;

namespace SampleProject.Application.Tasks.Common;
public record TasksFilter (string? title,
                           Status? Status,
                           PriorityLevel? PriorityLevel,
                           DateTime? duoDate,
                           int PageSize,
                           int PageNumber,
                           TasksOrderByEnum orderBy,
                           bool isDesc);

