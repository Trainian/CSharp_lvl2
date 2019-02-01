using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Workers_salary
{
    public class XmlSerializ
    {
        public XmlSerializ()
        {

        }

        public static void Serialize(List<Worker> workers, string path)
        {
            List<Type> workerTypes = new List<Type>();
            foreach (Worker worker in workers)
            {
                Type type = worker.GetType();
                if (!workerTypes.Contains(type))
                {
                    workerTypes.Add(type);
                }
            }
            XmlSerializer serializer = new XmlSerializer(typeof(List<Worker>), workerTypes.ToArray());
            using (Stream stream = new FileStream($"{path}\\save.xml", FileMode.Create))
            {
                serializer.Serialize(stream, workers);
            }
        }

        public static List<Worker> Deserialize(List<Worker> workers, string path)
        {
            List<Worker> list;
            List<Type> workerTypes = new List<Type>();
            foreach (Worker worker in workers)
            {
                Type type = worker.GetType();
                if (!workerTypes.Contains(type))
                {
                    workerTypes.Add(type);
                }
            }
            XmlSerializer serializer = new XmlSerializer(typeof(List<Worker>), workerTypes.ToArray());
            using (Stream stream = new FileStream($"{path}\\save.xml", FileMode.Open))
            {
                list = (List<Worker>)serializer.Deserialize(stream);
            }

            return list;
        }
    }
}
