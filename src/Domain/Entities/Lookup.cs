using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Domain.Entities;
public class Lookup :BaseAuditableEntity
{
    public string Text { get; set; }
    public string TextAr { get; set; }
    public LookupTypeGroup LookupTypeGroup { get; set; }
}
