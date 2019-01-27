using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers_salary
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Worker[] workers = new Worker[]
            {
                new HHourlyRate("Evgeniy Vitoldovich", 1032),
                new HMonthlyRate("Alina Kotar", 12670),
                new HHourlyRate("Dmitriy Gorbovskiy", 206),
            };
            Console.WriteLine("До сортировки массива:");
            foreach (Worker worker in workers)
            {
                worker.Salary();
            }

            Console.WriteLine("\nПосле сортировки массива:");
            Array.Sort(workers);
            foreach (Worker worker in workers)
            {
                worker.Salary();
            }

            Console.WriteLine("\nА теперь перечисления при помощи собственного массива:");
            Worker workers2 = new Worker(3);
            foreach (var worker in workers2)
            {
                Console.WriteLine(worker);
            }

            Console.ReadKey();
        }
    }
}
