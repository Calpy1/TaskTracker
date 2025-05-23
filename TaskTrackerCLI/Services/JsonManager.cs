using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskTrackerCLI.Models;

namespace TaskTrackerCLI.Services
{
    class JsonManager
    {
        private readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Data", "tasks.json");

        public bool IsFirstStart => !File.Exists(filePath);

        public void SaveTasks(TaskItem newTask)
        {
            List<TaskItem> tasks = LoadTasks();
            tasks.Add(newTask);

            WriteTasksToFile(tasks);
        }

        public void WriteTasksToFile(List<TaskItem> tasks)
        {
            string? directoryPath = Path.GetDirectoryName(filePath);
            
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true,
            };
            string jsonString = JsonSerializer.Serialize(tasks, options);
            File.WriteAllText(filePath, jsonString);
        }

        public List<TaskItem> LoadTasks()
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File Not Found. Please make a first task");
                return new List<TaskItem>();
            }

            string jsonString = File.ReadAllText(filePath);
            List<TaskItem>? deserializedTasks = JsonSerializer.Deserialize<List<TaskItem>>(jsonString);

            if (deserializedTasks == null)
            {
                return new List<TaskItem>();
            }
            else
            {
                return deserializedTasks;
            }
        }
    }
}
