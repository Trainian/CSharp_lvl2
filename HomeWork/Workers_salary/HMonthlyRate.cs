using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers_salary
{
    [Serializable]
    public class HMonthlyRate : Worker
    {
        public HMonthlyRate(string fio, decimal payment, int mass) : base(fio, payment, mass)
        {
        }

        public HMonthlyRate ()
        { }

        public override void Salary()
        {
            Console.WriteLine($"Месячная зарплата: {Payment:C}");
        }
    }
}
