using daily_task.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace daily_task.Domain.Entities
{
    public class Task : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public Priority Priority { get; set; }
        public string Description { get; set; } = string.Empty;
        public Status Status { get; set; }
        public string Gold { get; set; } = string.Empty;
    }
}
