﻿ using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers_salary
{
    /// <summary>
    /// Предоставляет массив рабочих
    /// </summary>
    class Worker : IComparable, IEnumerable
    {
        private int[] mass;
        protected string FIO { get; }
        protected decimal Payment { get; set; }

        public Worker(string fio, decimal payment, int mass) : this(fio, payment)
        {
            Random rnd = new Random();
            this.mass = new int[mass];
            for (int i = 0; i < mass; i++)
            {
                this.mass[i] = i+ rnd.Next(1,100);
            }
        }

        protected Worker(string fio, decimal payment) :this(fio)
        {
            Payment = payment;
        }

        protected Worker(string fio)
        {
            FIO = fio;
        }

        /// <summary>
        /// Нужен для вывода месячной зарплаты
        /// </summary>
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
