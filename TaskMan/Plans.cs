using System;
using System.Collections.Generic;
using System.Text;

namespace TaskMan
{
    class Plans
    {
        //определяем конструктор по умолчанию явно
        public Plans()
        {
            this.done = false;
        }

        //создаем поля: дата, задача, выполнение
        public DateTime date;
        public string task;
        public bool done;


        //Метод сравнивающий 2 элемента и возвращающий -1, если меньше, 1 если больше и 0 если равны
        //необходим для сортировки коллекции Sort<>
        public int CompareDates(Plans task1, Plans task2)
        {
            return task1.date.CompareTo(task2.date);
        }
        //Метод возвращает Статус задачи в виде строки в зависимости от булевого значения done и текущей даты
        public string Status()
        {
            string status = "in process-";
            if (done == true)
            {
                status = "have done--";
            }
            else if (DateTime.Today > date)
            {
                status = "out of date";
            }
            return status;
        }

    }
}
