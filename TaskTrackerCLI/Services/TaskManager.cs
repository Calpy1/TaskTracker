using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerCLI.Models;
using MyTaskStatus = TaskTrackerCLI.Models.TaskStatus;

namespace TaskTrackerCLI.Services
{
    public class TaskManager : ITaskManager
    {
        public List<TaskItem> taskItems = new List<TaskItem>();
        private JsonManager _jsonManager = new JsonManager();

        public TaskManager()
        {
            taskItems = _jsonManager.LoadTasks();
        }

        public void ShowTasks()
        {
            taskItems.ForEach(item => Console.WriteLine(item));
        }

        public void AddTask()
        {
            Console.WriteLine("Add a task title: ");
            string title = Console.ReadLine();

            while (string.IsNullOrEmpty(title))
            {
                Console.WriteLine("Title cannot be empty. Please enter a task title: ");
                title = Console.ReadLine();
            }

            string originalTitle = title;
            int index = 1;

            while (taskItems.Any(t => t.Title == title))
            {
                title = originalTitle + index;
                index++;
            }

            Console.WriteLine("Add a description(optional): ");
            string? description = Console.ReadLine();

            DateTime createdAt = DateTime.Now.ToLocalTime();
            string formattedDate = createdAt.ToString("dd.MM.yy");

            string formattedDeadline = string.Empty;
            while (string.IsNullOrEmpty(formattedDeadline))
            {
                Console.WriteLine("Add days to deadline: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int days) && days >= 0)
                {
                    DateTime deadlineAt = DateTime.Now.AddDays(days);
                    formattedDeadline = deadlineAt.ToString("dd.MM.yy");
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }

            string taskPriority = ReadEmptyString("Choose a task priority (Low, Medium, High): ", "Task Priority");
            TaskPriority priority;

            switch (taskPriority)
            {
                case "low":
                    priority = TaskPriority.Low;
                    break;
                case "medium":
                    priority = TaskPriority.Medium;
                    break;
                case "high":
                    priority = TaskPriority.High;
                    break;
                default:
                    priority = TaskPriority.Undefined;
                    break;
            }

            string taskStatus = ReadEmptyString("Choose a task status (Pending, In Progress, Completed, Delayed): ", "Task Status");
            MyTaskStatus status;


            switch (taskStatus)
            {
                case "pending":
                    status = MyTaskStatus.Pending;
                    break;
                case "in progress":
                    status = MyTaskStatus.In_Progress;
                    break;
                case "completed":
                    status = MyTaskStatus.Completed;
                    break;
                case "delayed":
                    status = MyTaskStatus.Delayed;
                    break;
                default:
                    status = MyTaskStatus.Undefined;
                    break;
            }

            TaskItem taskItem = new TaskItem(title, description, formattedDate, formattedDeadline, priority, status);
            taskItems.Add(taskItem);

            _jsonManager.SaveTasks(taskItem);
            Console.WriteLine("Task added successfully!\n");
        }

        public void RemoveTasks(string userChoice)
        {
            //string[] removeTaskParts = removeTask.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var tasksToRemove = taskItems.FindAll(t => t.Title.Equals(userChoice, StringComparison.OrdinalIgnoreCase));

            if (tasksToRemove.Count == 0)
            {
                Console.WriteLine($"Task \"{userChoice}\" not found\n");
                return;
            }

            foreach (var task in tasksToRemove)
            {
                taskItems.Remove(task);
                Console.WriteLine($"Task \"{task.Title}\" successfully deleted\n");
            }

            _jsonManager.WriteTasksToFile(taskItems);
        }

        public void PrintTasks()
        {
            foreach (TaskItem taskItem in taskItems)
            {
                Console.WriteLine(taskItem);
            }
        }

        public string ReadEmptyString(string warningMessage, string callMethodName)
        {
            Console.WriteLine(warningMessage);
            string text = Console.ReadLine().ToLower();

            if (string.IsNullOrEmpty(text))
            {
                Console.WriteLine($"{callMethodName} cannot be empty\n");
                while (string.IsNullOrEmpty(text))
                {
                    text = Console.ReadLine();
                }
            }
            return text;
        }
    }
}
