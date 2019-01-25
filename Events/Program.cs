using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Worker worker = new Worker(0, "Dima", "Home");
            worker.WorkededHandler += (sender, s) =>
            {
                Console.WriteLine("Работа была завершена и на данный момент на счету: " + s.Sum + "\u20BD");
                Console.WriteLine($"Спасибо за работу {s.FirstName} в нашей организации:'{s.Organization}'");
            };
            worker.Worked(worker);
            worker.DoubleWorked(worker);
            Console.ReadKey();
        }

    }

    class Worker
    {
        public event EventHandler<Worker> WorkededHandler;
        public int Sum { get; set; }
        public string FirstName { get; set; }
        public string Organization { get; set; }

        public Worker(int sum, string firstName, string organization)
        {
            Sum = sum;
            FirstName = firstName;
            Organization = organization;
        }

        public void Worked(Worker worker)
        {
            Sum++;
            WorkededHandler?.Invoke(this, worker);
        }

        public void DoubleWorked(Worker worker)
        {
            Sum += 2;
            WorkededHandler?.Invoke(this, worker);
        }
    }
}
