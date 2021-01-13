using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TaskMan
{
    class TaskServise : ITaskServise
    {
        //Метод, в котором организован основной фунционал
        public void RunTask()
        {
            //сегодняшнее число
            DateTime today = DateTime.Now;
            Console.WriteLine("Today is {0}", today.ToString("d"));
            // объявляем коллекцию Задач
            var tasks = new List<Plans>()
            {
                //наполняем список
                new Plans{Task ="To do Request for courses", TaskDate = Convert.ToDateTime("2020.11.18")},
                new Plans{Task="To send the link",TaskDate=Convert.ToDateTime("2020.12.18"), HaveDone=true},
                new Plans{Task="To begin learning", TaskDate=Convert.ToDateTime("2021.01.18")},
                new Plans{Task="To start working as a developer", TaskDate=Convert.ToDateTime("2021.06.01")},
                new Plans{Task ="To do the test",TaskDate=Convert.ToDateTime("2020.12.20"), HaveDone=true}
            };
            //Выводим на экран список дел                       
            ShowTasks(tasks);
            //сортируем
            SortPlans(tasks);
            //опять выводим
            Console.WriteLine("Sorted list");
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


        //метод ввода и проверки Id задачи, возвращает индекс элемента в списке и его ID
        private int EnterId(List<Plans> plans, out int id)
        {
            int index = -1;
            bool idTry;
            bool idContain = false;
            string position;
            id = -1;
            do
            {
                Console.WriteLine("Enter ID or type 'cancel'");
                position = Console.ReadLine();
                if (position == "cancel")
                {
                    idTry = true;
                    idContain = true;

                }
                else
                {
                    idTry = Int32.TryParse(position, out id);
                    if (idTry == true)
                    {
                        index = 0;
                        foreach (var item in plans)
                        {
                            if (item.TaskId == id)
                            {
                                idContain = true;

                                break;
                            }
                            index++;
                        }
                    }
                }
            }
            while (idTry == false || idContain == false);
            return index;

        }
        //метод ввода и проверки корректной даты
        private DateTime EnterDate(DateTime currentDay)
        {
            DateTime setDay;
            bool dayError;
            do
            {
                Console.WriteLine("Enter the real date in format dd.mm.yyyy or press 'Enter' if you want to leave Date without changes");
                string day = Console.ReadLine();
                dayError = DateTime.TryParse(day, out setDay);
                if (String.IsNullOrEmpty(day))
                {
                    setDay = currentDay;
                    dayError = true;
                }
                else if (dayError && setDay < DateTime.Today)
                {
                    dayError = false;
                    Console.WriteLine("The Date is in the past");
                }
            }
            while (dayError == false);
            return setDay;
        }
        //Метод добавления нового элемента
        private void Add(List<Plans> tasks)
        {
            Console.WriteLine("Create Task. Enter the task");
            string taskString = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(taskString))
            {
                Console.WriteLine("You left empty task");
                taskString = "Empty task";
            }
            Console.WriteLine("Current date is {0}", DateTime.Today.ToShortDateString());
            DateTime dateTemp = EnterDate(DateTime.Today);

            //создаем задачу на основе введенных данных

            Plans task = new Plans() { Task = taskString, TaskDate = dateTemp };

            //вставляем задачу в нужное место в отсортированную коллекцию
            //добавление элемента по индексу в заранее отстортированный список будет происходить быстрее, чем добавление в конец, а потом сортировка       

            if (dateTemp < tasks[tasks.Count - 1].TaskDate)
            {
                foreach (var item in tasks)
                {
                    if (dateTemp < item.TaskDate)
                    {
                        tasks.Insert(tasks.IndexOf(item), task);
                        //Console.WriteLine("New Task is created. ID {0}", task.TaskId);
                        break;
                    }
                }
            }
            else
            {
                tasks.Add(task);
                //Console.WriteLine("New Task is created. ID {0}", task.TaskId);
            }
            Console.WriteLine("New Task is created. ID {0}", task.TaskId);

        }


        //метод выводит на экран список задач
        private void ShowTasks(List<Plans> plans)
        {
            foreach (var item in plans)
            {
                string status = item.Status();
                Console.WriteLine("ID-{0}-{1}-{2}---{3}", item.TaskId, item.TaskDate.ToShortDateString(), status, item.Task);
            }
            Console.WriteLine(new string('-', 45));
        }

        //Метод сравнивающий 2 элемента и возвращающий -1, если меньше, 1 если больше и 0 если равны
        //необходим для сортировки коллекции Sort<>
        private int CompareDates(Plans task1, Plans task2)
        {
            return task1.TaskDate.CompareTo(task2.TaskDate);
        }


        //сортировака коллекции
        private void SortPlans(List<Plans> plans)
        {
            plans.Sort(CompareDates);
        }


        //удаление задачи
        private void DeleteTask(List<Plans> plans)
        {
            Console.WriteLine("Delete Task");
            int posi = EnterId(plans, out int idCurrent);
            if (posi != -1)
            {
                plans.RemoveAt(posi);
                Console.WriteLine("Task ID {0} Deleted", idCurrent);
            }
        }


        //Изменение задачи
        private void ChangeTask(List<Plans> plans)
        {
            int index = EnterId(plans, out int idCurrent);

            if (index != -1)
            {
                Console.WriteLine("Change Task ID {0}", idCurrent);
                Console.WriteLine("date---{0}", plans[index].TaskDate.ToShortDateString());
                Console.WriteLine("task---{0}", plans[index].Task);
                Console.WriteLine("status-{0}", plans[index].Status());

                var temp = plans[index].TaskDate;
                plans[index].TaskDate = EnterDate(plans[index].TaskDate);
                Console.WriteLine("Current date--{0}", plans[index].TaskDate.ToShortDateString());

                Console.WriteLine("Enter new Task");
                string taskString = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(taskString)) Console.WriteLine("You left empty space. Changes have not done");
                else plans[index].Task = taskString;
                Console.WriteLine("Current task--{0}", plans[index].Task);

                Console.WriteLine("Press 'Enter' if you want leave Status without changes");
                Console.WriteLine("Write 'done' if the Task is finished, other answer means 'have not done'");
                string change = Console.ReadLine();
                if (String.IsNullOrEmpty(change) != true)
                {
                    if (change == "done") plans[index].HaveDone = true;
                    else plans[index].HaveDone = false;
                }
                Console.WriteLine("Current status--{0}", plans[index].Status());
                if (plans[index].TaskDate != temp) SortPlans(plans);
            }
        }
    }
}
