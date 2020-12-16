using System;
using System.Collections.Generic;
using System.Text;

namespace TaskMan
{
    class Manager
    {
        //метод ввода и проверки порядковых номеров от 1 до maxValue
        private static int EnterNumber(int maxValue)
        {
            int index;
            bool indexTry;
            string pos;
            do
            {
                Console.WriteLine("Enter int number between {0} and {1}", 1, maxValue);
                pos = Console.ReadLine();
                indexTry = Int32.TryParse(pos, out index);
            }
            while (indexTry == false || index <= 0 || index > maxValue);
            return index;

        }
        //метод ввода и проверки корректной даты
        private static DateTime EnterDate()
        {
            DateTime dateD = DateTime.MinValue;
            bool dayError;
            do
            {
                Console.WriteLine("Enter the real date in format dd.mm.yyyy or press 'Enter' for today date");
                string day = Console.ReadLine();
                if (String.IsNullOrEmpty(day))
                {
                    dateD = DateTime.Today;
                    dayError = true;
                }
                else dayError = DateTime.TryParse(day, out dateD);
            }
            while (dayError == false);
            return dateD;
        }
        //Метод добавления нового элемента
        public static void Add(List<Plans> tasks)
        {
            Console.WriteLine("Create Task. Enter the task");
            string taskString = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(taskString))
            {
                Console.WriteLine("You left empty task");
                taskString = "Empty task";
            }
            DateTime dateD = EnterDate();

            //создаем задачу на основе введенных данных

            Plans task = new Plans() { task = taskString, date = dateD };

            //вставляем задачу в нужное место в отсортированную коллекцию
            //добавление элемента по индексу в заранее отстортированный список будет происходить быстрее, чем добавление в конец, а потом сортировка       

            if (dateD < tasks[tasks.Count - 1].date)
            {
                foreach (var item in tasks)
                {
                    if (dateD < item.date)
                    {
                        tasks.Insert(tasks.IndexOf(item), task);
                        Console.WriteLine("New Task is created. Position {0}", tasks.IndexOf(item));
                        break;
                    }
                }
            }
            else
            {
                tasks.Add(task);
                Console.WriteLine("New Task is created. Last position {0}", tasks.Count);
            }
        }
        //метод выводит на экран список задач
        public static void ShowTasks(List<Plans> plans)
        {
            //DateTime today = DateTime.Today;
            int pos;
            foreach (var item in plans)
            {
                string status = item.Status();
                pos = plans.IndexOf(item) + 1;
                Console.WriteLine("{0}-{1}-{2}---{3}", pos, item.date.ToShortDateString(), status, item.task);
            }
            Console.WriteLine(new string('-', 45));
        }

        //сортировака коллекции
        public static void SortPlans(List<Plans> plans)
        {
            plans.Sort(new Plans().CompareDates);
        }
        //удаление задачи
        public static void DeleteTask(List<Plans> plans)
        {
            Console.WriteLine("Delete Task");
            int length = plans.Count;
            int index = EnterNumber(length);
            plans.RemoveAt(index - 1);
            Console.WriteLine("Task {0} Deleted", index);
        }
        //Изменение задачи
        public static void ChangeTask(List<Plans> plans)
        {
            int length = plans.Count;
            int index = EnterNumber(length) - 1;

            Console.WriteLine("Change Task");
            Console.WriteLine("date---{0}", plans[index].date.ToShortDateString());
            Console.WriteLine("task---{0}", plans[index].task);
            Console.WriteLine("status-{0}", plans[index].Status());

            var temp = plans[index].date;
            plans[index].date = EnterDate();
            Console.WriteLine("Current date--{0}", plans[index].date.ToShortDateString());
            if (plans[index].date != temp) SortPlans(plans);

            Console.WriteLine("Enter new Task");
            string taskString = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(taskString)) Console.WriteLine("You left empty space. Changes have not done");
            else plans[index].task = taskString;
            Console.WriteLine("Current task--{0}", plans[index].task);

            Console.WriteLine("Press 'Enter' if you want leave Status without changes");
            Console.WriteLine("Write 'done' if the Task is finished, other answer means 'have not done'");
            string change = Console.ReadLine();
            if (String.IsNullOrEmpty(change) != true)
            {
                if (change == "done") plans[index].done = true;
                else plans[index].done = false;
            }

            Console.WriteLine("Current status--{0}", plans[index].Status());
        }
    }
}
