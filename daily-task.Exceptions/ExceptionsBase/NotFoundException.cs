namespace daily_task.Exceptions.ExceptionsBase
{
    public class NotFoundException : DailyTaskException
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public override IList<string> GetErrorMessages() => [Message];
    }
}