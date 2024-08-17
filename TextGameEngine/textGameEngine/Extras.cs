using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace textGameEngine
{
    internal class Extras
    {
        public static void WriteIf(bool condition, string write)
        {
            if (condition) { Console.Write(write); }
        }

        public static void WriteLineIf(bool condition, string writeLine)
        {
            if (condition) { Console.WriteLine(writeLine); }
        }
    }
}
