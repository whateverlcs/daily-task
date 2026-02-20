using daily_task.Domain.Enums;

namespace daily_task.Application.Models
{
    public class NewTask
    {
        public string Name { get; set; }
        public Priority Priority { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
    }
}