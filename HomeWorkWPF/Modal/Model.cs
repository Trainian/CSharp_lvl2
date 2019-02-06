using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HomeWorkWPF.Annotations;

namespace HomeWorkWPF
{
    class Model
    {
        public static ObservableCollection<Department> ListDepartments { get; set; } = new ObservableCollection<Department>();
        public static ObservableCollection<Employee> ListEmployees { get; set; } = new ObservableCollection<Employee>();

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
