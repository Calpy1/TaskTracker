using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerCLI.Services;

namespace TaskTrackerCLI.UI
{
    class Menu
    {
        TaskManager taskManager = new TaskManager();
        JsonManager jsonManager = new JsonManager();

        public void Run()
        {
            if (jsonManager.IsFirstStart)
            {
                Console.WriteLine("Lets make a first task!");
                taskManager.AddTask();
            }

            while (true)
            {
                Console.WriteLine("Choose option:\n" +
                    "Type \"ADD\" to add a task\n" +
                    "Type \"REMOVE <Task Name>\" to remove a task\n" +
                    "Type \"SHOW\" to show all tasks\n");

                string userInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("Empty input\n");
                    continue;
                }

                if (userInput.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                userInput = userInput.ToLower();
                string[] userInputParts = userInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (userInputParts[0].Equals("remove") && userInputParts.Length == 2)
                {
                    taskManager.RemoveTasks(userInputParts[1]);
                }
                else
                {
                    switch (userInputParts[0])
                    {
                        case "add":
                            taskManager.AddTask();
                            break;
                        case "show":
                            taskManager.ShowTasks();
                            break;
                        default:
                            Console.WriteLine("Invalid command\n");
                            break;
                    }
                }
            }

        }
    }
}
