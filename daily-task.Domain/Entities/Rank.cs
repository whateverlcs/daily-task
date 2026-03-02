using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace daily_task.Domain.Entities
{
    public class Rank : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public int TaskGoal { get; set; }
    }
}
