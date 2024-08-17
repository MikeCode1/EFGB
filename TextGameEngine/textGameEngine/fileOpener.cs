using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace textGameEngine
{
    internal static class fileOpener
    {
        public static string[] openFile(bool progressPrinting)
        {
            Console.WriteLine("Enter file path or exit with (exit or quit or esc or escape)");
            StreamReader file;
            List<string> lines = new List<string>();
            while (true)
            {
                string input = Console.ReadLine();
                if (input.ToLower() == "esc" || input.ToLower() == "exit" || input.ToLower() == "quit" || input.ToLower() == "escape")
                {
                    return new string[] { };
                }
                try
                {
                    Extras.WriteLineIf(progressPrinting, "Opening file.");
                    file = new StreamReader(input);
                    break;
                }
                catch
                {
                    Extras.WriteLineIf(progressPrinting, "Failed.");
                }
            }
            Extras.WriteLineIf(progressPrinting, "Success!");
            Extras.WriteLineIf(progressPrinting, "Reading line.");
            string line = file.ReadLine();
            while (line != null)
            {
                lines.Add(line);
                line = file.ReadLine();
            }
            Extras.WriteLineIf(progressPrinting, "Closing file.");
            file.Close();
            return lines.ToArray();
        }
    }
}
