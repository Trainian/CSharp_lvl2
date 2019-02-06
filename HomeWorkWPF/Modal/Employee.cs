using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HomeWorkWPF
{
    public class Employee : IValueConverter
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

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Model.ListDepartments[value is int ? (int) value : 0].Title;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
