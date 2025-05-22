using TaskTrackerCLI.Models;
using TaskTrackerCLI.Services;
using TaskTrackerCLI.UI;

namespace TaskTrackerCLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.Visualisation();
        }
    }
}
