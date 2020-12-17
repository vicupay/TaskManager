using System;
using System.Collections.Generic;
using System.Text;

namespace TaskMan
{
    class Plans
    {
        //создаем свойства: дата, задача, выполнение
        public int TaskId { get; set; }
        public DateTime TaskDate { get; set; }
        public string Task { get; set; }
        public bool HaveDone { get; set; }

        static private int IdLast = 0;
        //определяем конструктор по умолчанию явно
        public Plans()
        {
            IdLast++;
            this.HaveDone = false;
            this.TaskId = IdLast;
            
        }



        //Метод возвращает Статус задачи в виде строки в зависимости от булевого значения done и текущей даты
        public string Status()
        {
            string status = "in process-";
            if (HaveDone == true)
            {
                status = "have done--";
            }
            else if (DateTime.Today > TaskDate)
            {
                status = "out of date";
            }
            return status;
        }

    }
}
