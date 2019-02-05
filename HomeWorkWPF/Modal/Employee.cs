using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkWPF
{
    public class Employee
    {
        public string FIO { get; set; } = "Фамилия Имя Отчество";
        public int Age { get; set; } = 0;

        public int DepartmentId { get; set; } = 0;

        public Employee()
        {

        }

        public Employee(string fio, int age, int departmentId)
        {
            FIO = fio;
            Age = age;
            DepartmentId = departmentId;
        }

        public override string ToString()
        {
            return $"{FIO}, возраст: {Age}, компания: {Model.ListDepartments[DepartmentId]} ";
        }
    }
}
