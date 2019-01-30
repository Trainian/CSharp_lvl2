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
                new HHourlyRate("Evgeniy Vitoldovich", 1032, 10),
                new HMonthlyRate("Alina Kotar", 12670, 10),
                new HHourlyRate("Dmitriy Gorbovskiy", 206, 10),
            };
            Console.WriteLine("До сортировки массива:");
            foreach (Worker worker in workers)
            {
                worker.Salary();
            }

            Console.WriteLine("\nПосле сортировки массива, с использованием IComparable:");
            Array.Sort(workers);
            foreach (Worker worker in workers)
            {
                worker.Salary();
            }

            Console.WriteLine("А теперь используем перечислитель IEnumerator");
            foreach (var i in workers[0])
            {
                Console.WriteLine(i);
            }

            Console.WriteLine("Теперь проведем сравнение двух классов worker:");
            Console.WriteLine(workers[0].CompareTo(workers[1]));

            Console.ReadKey();
        }
    }
}
