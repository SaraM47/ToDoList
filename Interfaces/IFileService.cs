using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.Interfaces
{
    // Defines methods for saving and loading tasks.
    public interface IFileService
    {
        List<TaskItem> LoadTasks(string filePath); // Loads data from JSON file
        void SaveTasks(string filePath, List<TaskItem> tasks); // Saves data to JSON file
    }
}
