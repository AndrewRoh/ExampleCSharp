using System;

namespace CSarpEx
{
    public record Person1
    {
        public string Name { get; }
        public int Age { get; }
        public Person1(string name, int age) 
            => (Name, Age) = (name, age);
        public void Deconstruct(out string name, out int age)
            => (name, age) = (Name, Age);
    }

    public record Person2
    {
        public string Name { get; init; }
        public int Age { get; init; }
    }

    public record Employee : Person2
    {
        public int Id { get; init; }
    }
    class Ex09_1
    {
        static void Main09_1(string[] args)
        {
            // Constructor 사용
            Person1 p1 = new Person1("Tom", 30);

            // Deconstructor 사용
            var (name, age) = p1;
            Console.WriteLine($"{name}, {age}");

            Person2 p2 = new Person2
            {
                Name = "Tom",
                Age = 30
            };

            var tom2 = p2 with { Age = 40 };

            bool same = p1.Equals(p2);

            Person2 p3 = new Employee
            {
                Id = 1001,
                Name = "Tom",
                Age = 45
            };

            Console.WriteLine("Hello World!");
        }
    }
}
