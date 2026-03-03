using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace daily_task.Infrastructure.Migrations
{
    public abstract class DatabaseVersions
    {
        public const int TABLE_TASK = 1;
        public const int ADD_GOLD_TABLE_TASK = 2;
        public const int TABLE_RANK = 3;
        public const int TABLE_PROFILE = 4;
        public const int TABLE_REWARD = 5;
        public const int ADD_USER_TABLE_PROFILE = 6;
    }
}
