using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleProject.Domain.Entities;
public class UserTask : BaseAuditableEntity
{
    public  string Title { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; }
    public PriorityLevel Priority { get; set; }
    public DateTime DueDate { get; set; }
    public string UserId { get; set; }
    public virtual ApplicationUser User { get; set; }
}
