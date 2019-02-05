using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkWPF
{
    class Model
    {
        public static List<Department> ListDepartments = new List<Department>();
        public static List<Employee> ListEmployees = new List<Employee>();

        public static void CreateModel()
        {
            ListEmployees.Add(new Employee(@"Горбовский Дмитрий Владимирович", 27,1));
            ListEmployees.Add(new Employee(@"Горбовская Алина Сергеевна", 24,2));
            ListEmployees.Add(new Employee(@"Фамилия Имя Отчество", 0,0));

            ListDepartments.Add(new Department(0, @"Empty"));
            ListDepartments.Add(new Department(1,@"Yandex"));
            ListDepartments.Add(new Department(2, @"Google"));
        }
    }
}
