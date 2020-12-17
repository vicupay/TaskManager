using System;
using System.Collections.Generic;

namespace TaskMan
{
    class Program
    {
        static void Main(string[] args)
        {
            ITaskServise taskServise = new TaskServise();
            taskServise.RunTask();
        }
    }
}
