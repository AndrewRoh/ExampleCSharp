using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamCSarpStudy.Ex09
{
    public class Person
    {
        public Guid Id { get; init; }
        public string Name { get; }

        public Person(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
        }
    }

    public class Score
    {
        private readonly int category;
        private int val;

        public int Value
        {
            get
            {
                return val;
            }
            init
            {
                category = 1;
                val = value;
            }
        }
    }

    class Ex09_2
    {
        public static void Main09_02()
        {
            Person p1 = new Person("Tom");

            var s = new Score()
            {
                Value = 90
            };
        }
    }                                                                               
}
