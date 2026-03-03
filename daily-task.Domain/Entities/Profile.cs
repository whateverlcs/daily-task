namespace daily_task.Domain.Entities
{
    public class Profile : EntityBase
    {
        public string User { get; set; } = string.Empty;
        public int TasksCreated { get; set; }
        public int TasksCompleted { get; set; }
        public long GoldEarned { get; set; }
        public long GoldSpent { get; set; }
        public long GoldBalance { get; set; }
        public int ClaimedRewards { get; set; }
        public long RankId { get; set; }
    }
}