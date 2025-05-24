using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerCLI.Services
{
    interface ITaskManager
    {
        void ShowTasks();
        void AddTask();
        void RemoveTasks(string userChoice);
        void PrintTasks();
    }
}
