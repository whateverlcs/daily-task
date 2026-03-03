namespace daily_task.Domain.Entities
{
    public class Reward : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string Gold { get; set; } = string.Empty;
    }
}