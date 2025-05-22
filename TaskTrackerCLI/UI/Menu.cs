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
            TaskManager taskManager = new TaskManager();
            taskManager.AddTask();

            foreach (var item in taskManager.taskItems)
            {
                Console.WriteLine(item);
            }
        }
    }
}
