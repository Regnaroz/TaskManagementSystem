using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SampleProject.Domain.StoredProcedureObjects;
public class UserTaskCountDto
{
    public string UserId { get; set; }
    public int TaskCount { get; set; }
}
