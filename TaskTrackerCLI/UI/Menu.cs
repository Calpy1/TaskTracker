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
        public void Visualisation()
        {
            JsonManager jsonManager = new JsonManager();
            if (jsonManager.IsFirstStart)
            {
                Console.WriteLine("Lets make a first task!");
            }

            TaskManager taskManager = new TaskManager();
            //taskManager.LoadTasks();
            //taskManager.PrintTasks();
            taskManager.AddTask();
        }
    }
}
