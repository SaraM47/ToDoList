using System;
using System.Collections.Generic;
using ToDoList.Interfaces; // References the interface definitions for services
using ToDoList.Models; // Contains data models such as TaskItem
using ToDoList.Services; // Contains the logic that implements the interfaces

namespace ToDoList
{
    class Program
    {
        static void Main(string[] args)
        {
            // A custom title for the console window
            Console.Title = "To-Do List";
            string filePath = "Data/tasks.json";

            // Create service objects that handle file operations and task logic
            IFileService fileService = new FileService();
            ITaskService taskService = new TaskService(fileService);

            // Load previously saved tasks when the program starts
            List<TaskItem> tasks = fileService.LoadTasks(filePath);

            // The main program loop will continue running until the user chooses to exit
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=======================================");
                Console.WriteLine("             TO-DO LIST     ");
                Console.WriteLine("=======================================\n");
                Console.ResetColor();

                // Display all available menu options to the user
                Console.WriteLine("1. Visa alla uppgifter");
                Console.WriteLine("2. Lägg till ny uppgift");
                Console.WriteLine("3. Markera uppgift som klar");
                Console.WriteLine("4. Ta bort uppgift");
                Console.WriteLine("5. Sök uppgift");
                Console.WriteLine("6. Exportera uppgifter till textfil");
                Console.WriteLine("7. Avsluta");
                Console.Write("\nVälj ett alternativ (1–7): ");

                string? choice = Console.ReadLine();
                /*
                *Handle user input with a switch statement by show, adding, mark a specific task as completed, remove a selected task from the list, search tasks based on keywords or status or export all tasks to a text file for backup or printing
                */
                switch (choice)
                {
                    case "1":
                        taskService.ShowTasks(tasks);
                        break;
                    case "2":
                        taskService.AddTask(tasks);
                        break;
                    case "3":
                        taskService.MarkTaskAsCompleted(tasks);
                        break;
                    case "4":
                        taskService.DeleteTask(tasks);
                        break;
                    case "5":
                        taskService.SearchTasks(tasks);
                        break;
                    case "6":
                        taskService.ExportTasksToText(tasks);
                        break;
                    case "7":
                        running = false;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ogiltigt val! Ange ett tal mellan 1–7.");
                        Console.ResetColor();
                        Console.WriteLine("Tryck valfri tangent för att försöka igen.");
                        Console.ReadKey();
                        break;
                }
            }
            // Show a goodbye message when the program ends
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nTack för att du använde To-Do List applikationen!");
            Console.ResetColor();
        }
    }
}
