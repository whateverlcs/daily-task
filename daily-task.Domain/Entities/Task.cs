using daily_task.Domain.Enums;

namespace daily_task.Domain.Entities
{
    public class Task : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public Priority Priority { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Gold { get; set; } = string.Empty;
    }
}