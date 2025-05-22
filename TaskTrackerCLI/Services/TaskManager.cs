using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerCLI.Models;
using MyTaskStatus = TaskTrackerCLI.Models.TaskStatus;

namespace TaskTrackerCLI.Services
{
    class TaskManager
    {
        public List<TaskItem> taskItems = new List<TaskItem>();
        private JsonManager jsonManager = new JsonManager();
        public void AddTask()
        {
            Console.WriteLine("Add a task title: ");
            string title = Console.ReadLine();

            Console.WriteLine("Add a description(optional): ");
            string? description = Console.ReadLine();
            DateTime createdAt = DateTime.Now.ToLocalTime(); //!!
            string formattedDate = createdAt.ToString("dd.mm.yy");

            Console.WriteLine("Add a days to deadline: ");
            int days = int.Parse(Console.ReadLine());

            DateTime deadlineAt = DateTime.Now.AddDays(days);
            string formattedDeadline = deadlineAt.ToString("dd.mm.yy");

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

            string taskStatus = ReadEmptyString("Choose a task status (Pending, In Progress, Completed, Delayed): ", "Task status");
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
            jsonManager.SaveTasks(taskItem);
        }

        public void LoadTask()
        {
            taskItems = jsonManager.LoadTasks();
        }

        public string ReadEmptyString(string warningMessage, string callMethodName)
        {
            Console.WriteLine(warningMessage);
            string text = Console.ReadLine().ToLower();

            if (string.IsNullOrEmpty(text))
            {
                Console.WriteLine($"{callMethodName} cannot be empty");
                while (string.IsNullOrEmpty(text))
                {
                    text = Console.ReadLine();
                }
            }
            return text;
        }
    }
}
