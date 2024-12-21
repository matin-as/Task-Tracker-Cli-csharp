using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;
class Task
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string Status { get; set; } // "todo", "in-progress", "done"
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

class Program
{
    private static string filePath = "tasks.json";

    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide a command.");
            return;
        }

        string command = args[0].ToLower();

        switch (command)
        {
            case "add":
                if (args.Length < 2) Console.WriteLine("Usage: task-cli add \"description\"");
                else AddTask(string.Join(" ", args.Skip(1)));
                break;
            case "update":
                if (args.Length < 3) Console.WriteLine("Usage: task-cli update <id> \"description\"");
                else UpdateTask(int.Parse(args[1]), string.Join(" ", args.Skip(2)));
                break;
            case "delete":
                if (args.Length < 2) Console.WriteLine("Usage: task-cli delete <id>");
                else DeleteTask(int.Parse(args[1]));
                break;
            case "mark-in-progress":
                if (args.Length < 2) Console.WriteLine("Usage: task-cli mark-in-progress <id>");
                else UpdateTaskStatus(int.Parse(args[1]), "in-progress");
                break;
            case "mark-done":
                if (args.Length < 2) Console.WriteLine("Usage: task-cli mark-done <id>");
                else UpdateTaskStatus(int.Parse(args[1]), "done");
                break;
            case "list":
                if (args.Length < 2) ListTasks();
                else ListTasksByStatus(args[1].ToLower());
                break;
            default:
                Console.WriteLine("Unknown command.");
                break;
        }
    }

    private static List<Task> LoadTasks()
    {
        if (!File.Exists(filePath)) return new List<Task>();
        string json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<List<Task>>(json) ?? new List<Task>();
    }

    private static void SaveTasks(List<Task> tasks)
    {
        string json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    private static void AddTask(string description)
    {
        var tasks = LoadTasks();
        int newId = tasks.Any() ? tasks.Max(t => t.Id) + 1 : 1;
        var newTask = new Task
        {
            Id = newId,
            Description = description,
            Status = "todo",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        tasks.Add(newTask);
        SaveTasks(tasks);
        Console.WriteLine($"Task added successfully (ID: {newId})");
    }

    private static void UpdateTask(int id, string description)
    {
        var tasks = LoadTasks();
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            Console.WriteLine("Task not found.");
            return;
        }
        task.Description = description;
        task.UpdatedAt = DateTime.Now;
        SaveTasks(tasks);
        Console.WriteLine("Task updated successfully.");
    }

    private static void DeleteTask(int id)
    {
        var tasks = LoadTasks();
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            Console.WriteLine("Task not found.");
            return;
        }
        tasks.Remove(task);
        SaveTasks(tasks);
        Console.WriteLine("Task deleted successfully.");
    }

    private static void UpdateTaskStatus(int id, string status)
    {
        var tasks = LoadTasks();
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            Console.WriteLine("Task not found.");
            return;
        }
        task.Status = status;
        task.UpdatedAt = DateTime.Now;
        SaveTasks(tasks);
        Console.WriteLine("Task status updated successfully.");
    }

    private static void ListTasks()
    {
        var tasks = LoadTasks();
        if (!tasks.Any())
        {
            Console.WriteLine("No tasks found.");
            return;
        }
        foreach (var task in tasks)
        {
            Console.WriteLine($"[{task.Id}] {task.Description} - {task.Status} (Created: {task.CreatedAt}, Updated: {task.UpdatedAt})");
        }
    }

    private static void ListTasksByStatus(string status)
    {
        var tasks = LoadTasks().Where(t => t.Status == status).ToList();
        if (!tasks.Any())
        {
            Console.WriteLine($"No tasks with status '{status}' found.");
            return;
        }
        foreach (var task in tasks)
        {
            Console.WriteLine($"[{task.Id}] {task.Description} - {task.Status} (Created: {task.CreatedAt}, Updated: {task.UpdatedAt})");
        }
    }
}
