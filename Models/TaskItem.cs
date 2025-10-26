using System;

namespace ToDoList.Models
{
    //The TaskItem class represents a single task in the to-do list, each TaskItem stores different information of responsibilities
    public class TaskItem
    {
        public int Id { get; set; } // Unique ID for each task
        public string Title { get; set; } = string.Empty; // Task title
        public string Description { get; set; } = string.Empty; // Short description of the task
        public DateTime DueDate { get; set; } // Due date
        public bool IsCompleted { get; set; } // Status (ready/not ready)

        // Overrides the default ToString() method, which defines how the task will appear when printed in the console.
        public override string ToString()
        {
            string status = IsCompleted ? "Klar" : "Ej klar";
            return $"{Id}. {Title} ({status}) - Förfallodatum: {DueDate:yyyy-MM-dd}";
        }
    }
}
