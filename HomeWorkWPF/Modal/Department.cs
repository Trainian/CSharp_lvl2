using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkWPF
{
    public class Department
    {
        public int Id { get; set; } = -1;
        public string Title { get; set; } = "Empty";

        public Department()
        {
        }

        public Department(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public override string ToString()
        {
            return $"{Title}";
        }
    }
}
