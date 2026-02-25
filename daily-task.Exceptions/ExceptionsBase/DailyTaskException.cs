using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace daily_task.Exceptions.ExceptionsBase
{
    public abstract class DailyTaskException : SystemException
    {
        protected DailyTaskException(string message) : base(message)
        {
        }

        public abstract IList<string> GetErrorMessages();
    }
}
