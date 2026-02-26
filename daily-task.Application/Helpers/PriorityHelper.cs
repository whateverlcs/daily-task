using daily_task.Application.Models;
using daily_task.Domain.Enums;

namespace daily_task.Application.Helpers
{
    public static class PriorityHelper
    {
        private static readonly Random _random = new Random();

        public static List<PriorityDisplayModel> GetPriorities()
        {
            return Enum.GetValues(typeof(Priority))
                .Cast<Priority>()
                .Select(p => new PriorityDisplayModel
                {
                    Id = (int)p,
                    Priority = p.ToString().Replace("_", " ")
                })
                .ToList();
        }

        public static int GetRandomGold(this Priority priority)
        {
            return priority switch
            {
                Priority.Very_Low => _random.Next(5, 11),
                Priority.Low => _random.Next(11, 21),
                Priority.Medium => _random.Next(21, 31),
                Priority.High => _random.Next(31, 51),
                Priority.Very_High => _random.Next(51, 101),
                _ => 0
            };
        }
    }
}