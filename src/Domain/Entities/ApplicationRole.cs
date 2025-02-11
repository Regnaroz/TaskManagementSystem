using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SampleProject.Domain.Entities;
public class ApplicationRole:IdentityRole
{
    public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; } 
    public RoleEnum EnumRoleId { get; set; }
}
