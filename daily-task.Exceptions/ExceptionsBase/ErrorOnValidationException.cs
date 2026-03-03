namespace daily_task.Exceptions.ExceptionsBase
{
    public class ErrorOnValidationException : DailyTaskException
    {
        private readonly IList<string> _errorMessages;

        public ErrorOnValidationException(IList<string> errorMessages) : base(string.Empty)
        {
            _errorMessages = errorMessages;
        }

        public override IList<string> GetErrorMessages() => _errorMessages;
    }
}