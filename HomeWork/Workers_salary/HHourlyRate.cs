using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers_salary
{
    class HHourlyRate : Worker
    {
        public HHourlyRate(string fio, decimal payment, int mass) : base(fio,payment,mass)
        {
            this.Payment = (decimal)20.8 * 8 * payment;
        }
        /// <summary>
        /// Выводит зарплату за месяц, а не за день.
        /// </summary>
        public override void Salary()
        {
            Console.WriteLine($"Месячная зарплата: {Payment:C}");
        }
    }
}
