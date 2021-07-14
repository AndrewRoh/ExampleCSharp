using System;

namespace ExamCSarpStudy.Ex09
{

    class Ex09_4
    {

        public static void Main09_4()
        {
            char g = GetGrade(75);
            Console.WriteLine(g);
        }

        static char GetGrade(int score)
        {
            // 관계 패턴
            char gr = score switch
            {
                >= 90 => 'A',
                >= 80 => 'B',
                >= 70 => 'C',
                >= 60 => 'D',
                _ => 'F'
            };
            return gr;
        }

        static int GetValue(int category)
        {
            // 관계 패턴과 논리 패턴을 혼합하여 사용
            int val = category switch
            {
                0 or 1 => 101,
                > 1 and < 10 => 201,
                not 100 => 301,
                _ => 401
            };
            return val;

            // if (!(a is Dog)) :기존방식
            //if (a is not Dog)   :C# 9    논리패턴 is not 키워드 사용가능
        }

        //void Check(Animal animal)
        //{
        //    string name = animal switch
        //    {
        //        Dog d => "Dog",
        //        Cat c => "Cat",
        //        //Animal _ => ""  -> C# 9에서는 어떤 타입의 변수를 사용하지 않는다면, 밑줄을 생략할 수 있게 되었다
        //        Animal => ""
        //    };
        //}
    }
}
