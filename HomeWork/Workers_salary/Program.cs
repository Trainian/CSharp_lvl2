using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
                new HMonthlyRate("Alina Kotar", 12670, 30),
                new HHourlyRate("Dmitriy Gorbovskiy", 206, 15),
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

            var workersList = workers.ToList();
            var findAll = from wor in workersList where wor.Payment > 20000 && wor.Payment <= 50000 select wor;
            foreach (var worker in findAll)
            {
                Console.WriteLine(worker);
            }


            Console.WriteLine("Теперь Сериализуем и Десериализуем список, после чего выводим его на консоль");
            XmlSerializ.Serialize(workersList, Directory.GetCurrentDirectory());
            Thread.Sleep(1000);
            List<Worker> listWorkers = XmlSerializ.Deserialize(workersList, Directory.GetCurrentDirectory());
            foreach (Worker worker in listWorkers)
            {
                Console.WriteLine(worker);
            }
            Console.ReadKey();
        }
    }
}
