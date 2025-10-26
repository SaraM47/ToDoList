using System;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Interfaces;
using ToDoList.Models;

/*
* The TaskService class handles all task-related logic: adding, viewing, marking as completed, deleting, searching, and exporting. It communicates with the FileService to save and load tasks from a JSON file.
*/
namespace ToDoList.Services
{
    public class TaskService : ITaskService
    {
        private readonly IFileService _fileService;
        private readonly string _filePath = "Data/tasks.json";

        // Constructor that receives a FileService object (through the IFileService interface)
        public TaskService(IFileService fileService)
        {
            _fileService = fileService;
        }

        // Add a new task
        public void AddTask(List<TaskItem> tasks)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== Lägg till ny uppgift ===\n");
            Console.ResetColor();

            // Ask for task details
            Console.Write("Titel: ");
            string title = Console.ReadLine() ?? "";

            Console.Write("Beskrivning: ");
            string description = Console.ReadLine() ?? "";

            Console.Write("Förfallodatum (YYYY-MM-DD): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime dueDate))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ogiltigt datumformat! Uppgiften sparas inte.");
                Console.ResetColor();
                return;
            }

            // Create a unique ID by taking the current highest ID and adding 1
            int newId = tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1;

            // Create a new TaskItem object
            var newTask = new TaskItem
            {
                Id = newId,
                Title = title,
                Description = description,
                DueDate = dueDate,
                IsCompleted = false,
            };
            // Add to list and save to file
            tasks.Add(newTask);
            _fileService.SaveTasks(_filePath, tasks);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nUppgift sparad!");
            Console.ResetColor();
            Console.WriteLine("Tryck [Enter] för att återgå till menyn.");
            Console.ReadLine();
        }

        // Show all tasks
        public void ShowTasks(List<TaskItem> tasks)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== Visa uppgifter ===\n");
            Console.ResetColor();

            // If there is no existing tasks
            if (tasks.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Inga uppgifter att visa.");
                Console.ResetColor();
                Console.WriteLine("\nTryck [Enter] för att återgå till menyn.");
                Console.ReadLine();
                return;
            }

            // Filtering options for the user
            Console.WriteLine("1. Visa alla uppgifter");
            Console.WriteLine("2. Visa endast ej klara uppgifter");
            Console.WriteLine("3. Visa endast klara uppgifter");
            Console.WriteLine("4. Sortera efter förfallodatum");
            Console.Write("\nVälj ett alternativ (1–4) eller tryck [Enter] för att återgå: ");

            string? option = Console.ReadLine();

            // If user just presses Enter, return to main menu
            if (string.IsNullOrWhiteSpace(option))
                return;

            IEnumerable<TaskItem> filteredTasks = tasks;

            // Filter tasks based on the selected option
            switch (option)
            {
                case "2":
                    filteredTasks = tasks.Where(t => !t.IsCompleted);
                    break;
                case "3":
                    filteredTasks = tasks.Where(t => t.IsCompleted);
                    break;
                case "4":
                    filteredTasks = tasks.OrderBy(t => t.DueDate);
                    break;
                case "1":
                    filteredTasks = tasks;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(
                        "\nOgiltigt val! Ange ett tal mellan 1–4 eller tryck [Enter] för att gå tillbaka."
                    );
                    Console.ResetColor();
                    Console.WriteLine("\nTryck [Enter] för att fortsätta.");
                    Console.ReadLine();
                    return;
            }
            // Display filtered results
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== Dina uppgifter ===\n");
            Console.ResetColor();

            foreach (var task in filteredTasks)
            {
                string status = task.IsCompleted ? "Klar" : "Ej klar";
                Console.WriteLine(
                    $"{task.Id}. {task.Title} - {status} (Förfallodatum: {task.DueDate:yyyy-MM-dd})"
                );
            }

            Console.WriteLine("\nTryck [Enter] för att återgå till menyn.");
            Console.ReadLine();
        }

        // Mark the task as completed
        public void MarkTaskAsCompleted(List<TaskItem> tasks)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== Markera uppgift som klar ===\n");
            Console.ResetColor();

            // Tip line to the user
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Tips: Använd ID-numret som visas i 'Visa uppgifter'-menyn.");
            Console.ResetColor();
            Console.WriteLine();

            Console.Write("Ange uppgifts-ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var task = tasks.FirstOrDefault(t => t.Id == id);
                if (task == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ingen uppgift hittades med det ID-numret.");
                    Console.ResetColor();
                }
                else
                {
                    task.IsCompleted = true; // Mark task as completed and save
                    _fileService.SaveTasks(_filePath, tasks);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Uppgift markerad som klar!");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ogiltig inmatning.");
                Console.ResetColor();
            }

            Console.WriteLine("\nTryck [Enter] för att återgå till menyn.");
            Console.ReadLine();
        }

        // Delete a task
        public void DeleteTask(List<TaskItem> tasks)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== Ta bort uppgift ===\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Tips: Använd ID-numret som visas i 'Visa uppgifter'-menyn.");
            Console.ResetColor();
            Console.WriteLine();

            Console.Write("Ange ID för uppgiften du vill ta bort: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var task = tasks.FirstOrDefault(t => t.Id == id);
                if (task == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ingen uppgift hittades med det ID-numret.");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write($"Bekräfta borttagning av \"{task.Title}\" (J/N): ");
                    string confirm = (Console.ReadLine() ?? "").ToUpper();

                    if (confirm == "J")
                    {
                        tasks.Remove(task);
                        _fileService.SaveTasks(_filePath, tasks);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Uppgift borttagen!");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Borttagning avbröts.");
                        Console.ResetColor();
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ogiltig inmatning.");
                Console.ResetColor();
            }

            Console.WriteLine("\nTryck [Enter] för att återgå till menyn.");
            Console.ReadLine();
        }

        // Search a task
        public void SearchTasks(List<TaskItem> tasks)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== Sök uppgift ===\n");
            Console.ResetColor();

            if (tasks.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Det finns inga uppgifter att söka efter.");
                Console.ResetColor();
                Console.WriteLine("\nTryck [Enter] för att återgå till menyn.");
                Console.ReadLine();
                return;
            }

            Console.Write("Skriv in sökord eller ID (lämna tomt för att avbryta): ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                return;

            IEnumerable<TaskItem> results = Enumerable.Empty<TaskItem>();

            // Determine if the search input is an ID or text
            if (int.TryParse(input, out int id))
            {
                results = tasks.Where(t => t.Id == id);
            }
            else
            {
                results = tasks.Where(t =>
                    t.Title.Contains(input, StringComparison.OrdinalIgnoreCase)
                    || t.Description.Contains(input, StringComparison.OrdinalIgnoreCase)
                );
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== Sökresultat ===\n");
            Console.ResetColor();

            if (!results.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Inga uppgifter hittades för din sökning.");
                Console.ResetColor();
            }
            else
            {
                foreach (var task in results)
                {
                    string status = task.IsCompleted ? "Klar" : "Ej klar";
                    Console.WriteLine(
                        $"{task.Id}. {task.Title} - {status} (Förfallodatum: {task.DueDate:yyyy-MM-dd})"
                    );
                }
            }

            Console.WriteLine("\nTryck [Enter] för att återgå till menyn.");
            Console.ReadLine();
        }

        // Export tasks to a textfile
        public void ExportTasksToText(List<TaskItem> tasks)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== Exportera uppgifter till textfil ===\n");
            Console.ResetColor();

            if (tasks.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Det finns inga uppgifter att exportera.");
                Console.ResetColor();
                Console.WriteLine("\nTryck [Enter] för att återgå till menyn.");
                Console.ReadLine();
                return;
            }
            // Ensure the export folder exists
            string exportFolder = "Exports";
            Directory.CreateDirectory(exportFolder); // Create a fold if it does not exist

            // Create a new filename based on the current timestamp
            string fileName = $"ToDoList_Export_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            string filePath = Path.Combine(exportFolder, fileName);

            // Write all task data into the text file
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("===== TO-DO LIST EXPORT =====");
                    writer.WriteLine($"Exportdatum: {DateTime.Now:yyyy-MM-dd HH:mm}");
                    writer.WriteLine("=============================\n");

                    foreach (var task in tasks.OrderBy(t => t.DueDate))
                    {
                        string status = task.IsCompleted ? "Klar" : "Ej klar";
                        writer.WriteLine($"ID: {task.Id}");
                        writer.WriteLine($"Titel: {task.Title}");
                        writer.WriteLine($"Beskrivning: {task.Description}");
                        writer.WriteLine($"Förfallodatum: {task.DueDate:yyyy-MM-dd}");
                        writer.WriteLine($"Status: {status}");
                        writer.WriteLine("-----------------------------");
                    }
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Uppgifterna har exporterats till: {filePath}");
                Console.ResetColor();
            }
            // If something goes wrong, show the error message
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Ett fel uppstod vid export: {ex.Message}");
                Console.ResetColor();
            }

            Console.WriteLine("\nTryck [Enter] för att återgå till menyn.");
            Console.ReadLine();
        }
    }
}
