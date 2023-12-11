namespace Todo_App.Models
{
    public class AddTodoDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime DeadlineDate { get; set; }
    }
}
