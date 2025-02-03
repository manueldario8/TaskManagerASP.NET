using System.ComponentModel.DataAnnotations;

namespace ToDoListWebApp.Models
{
    public class ToDoList
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Success { get; set; }


    }
}
