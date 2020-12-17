using System;
using System.Collections.Generic;

namespace TaskMan
{
    class Program : Manager
    {
        static void Main(string[] args)
        {
            //сегодняшнее число
            DateTime today = DateTime.Now;
            Console.WriteLine("Hello World! Today is {0}", today.ToString("d"));
            // объявляем коллекцию Задач
            var tasks = new List<Plans>()
            {
                //наполняем список
                new Plans{task ="To do Request for courses", date = Convert.ToDateTime("2020.11.18")},
                new Plans{task="To send the link",date=Convert.ToDateTime("2020.12.17"), done=true},
                new Plans{task="To begin learning", date=Convert.ToDateTime("2021.01.18")},
                new Plans{task="To start working as a developer", date=Convert.ToDateTime("2021.06.01")},
                new Plans{task = "To do the test",date=Convert.ToDateTime("2020.12.20"), done=true}
            };
            //Выводим на экран список дел
            ShowTasks(tasks);
            //сортируем
            SortPlans(tasks);
            //опять выводим
            ShowTasks(tasks);
            Console.WriteLine("Tasks are always sorted by date");
            string command;
            do
            {
                Console.WriteLine("You can do next actions: exit='exit', add='add', delete task='del', change task='edit', show list='show' ");
                Console.WriteLine("Do command");
                command = Console.ReadLine();
                if (command == "exit") break;

                switch (command)
                {
                    case "add":
                        Add(tasks);
                        break;
                    case "del":
                        DeleteTask(tasks);
                        break;
                    case "edit":
                        ChangeTask(tasks);
                        break;
                    case "show":
                        ShowTasks(tasks);
                        break;
                   // default: break;
                }
            }
            while (true);



        }
    }
}
