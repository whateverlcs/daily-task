namespace daily_task.Domain.Extensions
{
    public static class BooleanExtension
    {
        public static bool IsFalse(this bool value) => !value;
    }
}