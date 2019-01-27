using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers_salary
{
    class HHourlyRate : Worker
    {
        public HHourlyRate(string fio, decimal payment) : base(fio)
        {
            this.Payment = (decimal)20.8 * 8 * payment;
        }

        public override void Salary()
        {
            Console.WriteLine($"Месячная зарплата: {Payment:C}");
        }
    }
}
