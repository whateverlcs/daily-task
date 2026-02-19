using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace daily_task.Domain.Extensions
{
    public static class BooleanExtension
    {
        public static bool IsFalse(this bool value) => !value;
    }
}
