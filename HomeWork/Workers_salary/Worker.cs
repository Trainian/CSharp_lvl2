using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers_salary
{
    class Worker : IComparable, IEnumerable
    {
        private int[] mass;
        protected string FIO { get; }
        protected decimal Payment { get; set; }

        protected Worker(string fio, decimal payment)
        {
            FIO = fio;
            Payment = payment;
        }

        protected Worker(string fio)
        {
            FIO = fio;
        }

        public Worker(int mass)
        {
            this.mass = new int[mass];
            for (int i = 0; i < mass; i++)
            {
                this.mass[i] = i;
            }
        }

        public virtual void Salary()
        {

        }
        public int CompareTo(object obj)
        {
            if (this.Payment < (obj as Worker).Payment) return 1;
            if (this.Payment > (obj as Worker).Payment) return -1;
            return 0;
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < mass.Length; i++)
            {
                yield return mass[i];
            }
        }
    }
}
