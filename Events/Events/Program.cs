using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;

namespace Events
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            List<Worker> list = new List<Worker>();
            list.Add(new Worker(0, "Dima", "Home"));
            list.Add(new Worker(20, "Dima2", "Home2"));
            list.Add(new Worker(330, "Dima3", "Home3"));
            SaveAsXmlFormat(list, "data.xml");
            LoadFromXmlFormat(ref list, "data.xml");
            foreach (Worker i in list)
            {
                Console.WriteLine($"Имя: {i.FirstName}, Организация: {i.Organization}, Место работы: {i.Organization}");
            }
            //worker.WorkededHandler += (sender, s) =>
            //{
            //    Console.WriteLine("Работа была завершена и на данный момент на счету: " + s.Sum + "\u20BD");
            //    Console.WriteLine($"Спасибо за работу {s.FirstName} в нашей организации:'{s.Organization}'");
            //};
            //worker.Worked(worker);
            //worker.DoubleWorked(worker);
            //SaveAsXMLFormat(worker, "data.xml");
            //Worker newWorker = LoadFromXMLFormat("data.xml");
            //Console.WriteLine($"{newWorker.FirstName} {newWorker.Organization} {newWorker.Sum}");
            Console.ReadKey();
            Type type = new TypeDelegator(typeof(Worker));
            Console.ReadKey();
        }

        static void SaveAsXmlFormat(List<Worker> w, string fileName)
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Worker>));
            Stream str = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            xmlFormat.Serialize(str, w);
            str.Close();
        }

        static void LoadFromXmlFormat(ref List<Worker> w,string str)
        {
            if (w == null) throw new ArgumentNullException(nameof(w));
            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Worker>));
            Stream stream = new FileStream(str, FileMode.Open, FileAccess.Read);
            w = (List<Worker>)xmlFormat.Deserialize(stream);
            stream.Close();
        }
    }

    [Serializable]
    public class Worker
    {
        public event EventHandler<Worker> WorkededHandler;
        public int Sum { get; set; }
        public string FirstName { get; set; }
        public string Organization { get; set; }

        public Worker()
        {

        }

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
