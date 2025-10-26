using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ToDoList.Interfaces;
using ToDoList.Models;

namespace ToDoList.Services
{
    /*
        * The FileService class is responsible for reading (loading) and writing (saving) task data to and from a JSON file.
        */
    public class FileService : IFileService
    {
        // Load task from JSON-file
        public List<TaskItem> LoadTasks(string filePath)
        {
            try
            {
                // If the file does not exist, return an empty list so the program can still run without crashing.
                if (!File.Exists(filePath))
                {
                    return new List<TaskItem>();
                }
                // Read the entire JSON file as text
                string json = File.ReadAllText(filePath);
                var tasks = JsonSerializer.Deserialize<List<TaskItem>>(json); // Convert (deserialize) the JSON text back into a list of TaskItem objects

                return tasks ?? new List<TaskItem>(); // If returns null, return an empty list instead
            }
            // Handle any errors (for example: file is locked, invalid JSON, etc.)
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Ett fel uppstod vid inläsning av fil: {ex.Message}");
                Console.ResetColor();
                return new List<TaskItem>();
            }
        }

        // Save tasks to JSON-file
        public void SaveTasks(string filePath, List<TaskItem> tasks)
        {
            try
            {
                // Convert (serialize) the list of tasks into a JSON-formatted string
                string json = JsonSerializer.Serialize(
                    tasks,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true, // Makes the JSON file readable
                    }
                );

                File.WriteAllText(filePath, json); // Write the JSON string to the specified file path
            }
            // During file saving handles any errors that occur
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Kunde inte spara filen: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
