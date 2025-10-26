using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.Interfaces
{
    /*
    * Defines all the methods that a TaskService class must implement: Add a new task to the list, Display all tasks (with filtering options), mark a task as completed by its ID, delete a task from the list, search for a task by keyword or ID and export all tasks to a text file
    */
    public interface ITaskService
    {
        void AddTask(List<TaskItem> tasks);
        void ShowTasks(List<TaskItem> tasks);
        void MarkTaskAsCompleted(List<TaskItem> tasks);
        void DeleteTask(List<TaskItem> tasks);
        void SearchTasks(List<TaskItem> tasks);
        void ExportTasksToText(List<TaskItem> tasks);
    }
}
