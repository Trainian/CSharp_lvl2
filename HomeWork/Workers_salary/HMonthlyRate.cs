using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers_salary
{
    class HMonthlyRate : Worker
    {
        public HMonthlyRate(string fio, decimal payment) : base(fio, payment)
        {
        }

        public override void Salary()
        {
            Console.WriteLine($"Месячная зарплата: {Payment:C}");
        }
    }
}
