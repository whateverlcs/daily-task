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