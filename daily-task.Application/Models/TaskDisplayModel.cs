namespace daily_task.Application.Models
{
    public class TaskDisplayModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CreatedDate { get; set; } = string.Empty;
        public string Gold { get; set; } = string.Empty;
    }
}