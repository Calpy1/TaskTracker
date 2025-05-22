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

            Console.WriteLine("Add a deadline time (DD.MM.YY): ");

            DateTime deadlineAt;

            while (true) //!!
            {
                string timeReadling = Console.ReadLine();
                bool parseTimeSuccess = DateTime.TryParseExact(timeReadling, "dd.MM.yy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out deadlineAt);

                if (parseTimeSuccess)
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Invalid date format. Please use \"dd.MM.yy\"");
                }
            }

            string taskPriority = ReadEmptyString("Choose a task priority (Low, Medium, High): ", "Task Priority");
            TaskPriority priority;

            switch (taskPriority)
            {
                case "Low":
                    priority = TaskPriority.Low;
                    break;
                case "Medium":
                    priority = TaskPriority.Medium;
                    break;
                case "High":
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
                case "Pending":
                    status = MyTaskStatus.Pending;
                    break;
                case "In Progress":
                    status = MyTaskStatus.InProgress;
                    break;
                case "Completed":
                    status = MyTaskStatus.Completed;
                    break;
                case "Delayed":
                    status = MyTaskStatus.Delayed;
                    break;
                default:
                    status = MyTaskStatus.Undefined;
                    break;
            }

            TaskItem taskItem = new TaskItem(title, description, createdAt, deadlineAt, priority, status);
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
            string text = Console.ReadLine();

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
