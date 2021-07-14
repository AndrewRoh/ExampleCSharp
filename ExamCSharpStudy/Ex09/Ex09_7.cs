using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamCSarpStudy.Ex09
{
    class Ex09_7
    {
        public static void Main09_7()
        {
            nint a = 5;
            int b = 10;

            nint c = a + b;
            Console.WriteLine(typeof(nint));    // System.IntPtr

            long d = 15;
            if (a < d)
            {
                Console.WriteLine(a + d);   // 20
            }
        }

    }
}
