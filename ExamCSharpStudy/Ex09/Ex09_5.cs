using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamCSarpStudy.Ex09
{
    public class Employee2 { }
    public class Fulltime : Employee2 { }
    public class Parttime : Employee2 { }

    class Ex09_5
    {
        // 필드에서 new() 사용할 때 유용
        private Dictionary<string, string> hash = new();

        [STAThread]
        public static void Main09_5()
        {
            // var 사용
            var a = new Employee2();

            // new() 사용
            Employee2 b = new();

            Fulltime fte = null;
            Parttime part = new Parttime();
            bool ok = false;

            // Base 타입 공유
            Employee2 emp = ok ? fte : part;

            // nullable value type
            int? i = ok ? 0 : null;
        }
    }
}
