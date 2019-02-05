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

        public static void Serialize<T>(List<T> list, string path)
        {
            List<Type> allTypes = new List<Type>();
            foreach (T worker in list)
            {
                Type type = worker.GetType();
                if (!allTypes.Contains(type))
                {
                    allTypes.Add(type);
                }
            }
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>), allTypes.ToArray());
            using (Stream stream = new FileStream($"{path}\\save.xml", FileMode.Create))
            {
                serializer.Serialize(stream, list);
            }
        }

        public static List<T> Deserialize<T>(List<T> list, string path)
        {
            List<T> newlist;
            List<Type> workerTypes = new List<Type>();
            foreach (T worker in list)
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
                newlist = (List<T>)serializer.Deserialize(stream);
            }

            return newlist;
        }
    }
}
