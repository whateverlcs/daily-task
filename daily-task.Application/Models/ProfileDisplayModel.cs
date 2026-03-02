namespace daily_task.Application.Models
{
    public class ProfileDisplayModel
    {
        public int Id { get; set; }
        public int TasksCreated { get; set; }
        public int TasksCompleted { get; set; }
        public long GoldEarned { get; set; }
        public long GoldSpent { get; set; }
        public long GoldBalance { get; set; }
        public int ClaimedRewards { get; set; }
        public long RankId { get; set; }
        public string Rank { get; set; } = string.Empty;
    }
}