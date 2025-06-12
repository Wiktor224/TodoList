namespace TaskItem.Models
{
    public class Task
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public bool IsDone { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

