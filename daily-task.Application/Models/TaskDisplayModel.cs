namespace daily_task.Application.Models
{
    public class TaskDisplayModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Priority { get; set; }
        public string Description { get; set; }
        public string CreatedDate { get; set; }
    }
}