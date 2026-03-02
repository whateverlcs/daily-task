namespace daily_task.Application.Models
{
    public class RewardDisplayModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Gold { get; set; } = string.Empty;
        public bool Active { get; set; }
    }
}