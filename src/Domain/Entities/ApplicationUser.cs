using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SampleProject.Domain.Entities;
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool isActive { get; set; } = true;
    public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
    public virtual ICollection<UserTask> Tasks { get; set; }
}
