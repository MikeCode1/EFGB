using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static textGameEngine.Compiler;

namespace textGameEngine
{
    internal class Compiler
    {
        // Format:
        // ID: Text > Input: ID, Input: ID, ...;
        // ID: Text > Input: ID, Input: ID, ...;
        //...

        public static Chapter[] compile(string[] translatedFile, bool progressPrinting)
        {
            Extras.WriteLineIf(progressPrinting, "Comiling file.");
            StringBuilder lines = new StringBuilder("");
            int fileLineAmount = translatedFile.Length;
            for (int i = 0; i < fileLineAmount; i++)
            {
                try
                {
                    if (translatedFile[i][0] == '#' || translatedFile[i][0] == '/' && translatedFile[i][1] == '/')
                    {
                        continue;
                    }
                }
                catch { }
                lines.Append(translatedFile[i]);
                lines.Append("\n");
            }
            string lineFile = lines.ToString();
            string[] uncompiledChapters = lineFile.Split(';');
            int chapterAmount = uncompiledChapters.Length - 1;
            List<Chapter> chapters = new List<Chapter>();
            for (int i = 0; i < chapterAmount; i++)
            {
                try
                {
                    string[] idTextSlashPointers = cutFindFirst(uncompiledChapters[i], '>');
                    string[] idText = cutFindFirst(idTextSlashPointers[0], ':');
                    string[] uncompiledPointers = idTextSlashPointers[1].Split(',');

                    Chapter chapter = new Chapter();
                    chapter.id = idText[0].Trim();
                    string tempText = cut(idText[1], findFirst(idText[1], '"'))[1];
                    chapter.text = cutFindLast(tempText, '"')[0];
                    int pointAmount = uncompiledPointers.Length;
                    List<IdPointer> pointers = new List<IdPointer>();
                    // Good Upto this point
                    for (int j = 0; j < pointAmount; j++)
                    {
                        string[] inputId = uncompiledPointers[j].Split(':');
                        IdPointer pointer = new IdPointer();
                        pointer.input = inputId[0].Trim().ToLower();
                        pointer.id = inputId[1].Trim();
                        pointers.Add(pointer);
                    }
                    chapter.pointers = pointers.ToArray();
                    chapters.Add(chapter);
                }
                catch { }
            }
            Extras.WriteLineIf(progressPrinting, "Compiled.");
            return chapters.ToArray();
        }

        private static int[] find(string str, char ch)
        {
            int strLen = str.Length;
            List<int> indexes = new List<int>();
            for (int i = 0; i < strLen; i++)
            {
                if (str[i] == ch)
                {
                    indexes.Append(i);
                }
            }
            return indexes.ToArray();
        }

        private static int findFirst(string str, char ch)
        {
            int strLen = str.Length;
            for (int i = 0; i < strLen; i++)
            {
                if (str[i] == ch)
                {
                    return i;
                }
            }
            throw new Exception("Can't find char in string.");
        }

        private static int findLast(string str, char ch)
        {
            int strLen = str.Length;
            for (int i = strLen - 1; i >= 0; i--)
            {
                if (str[i] == ch)
                {
                    return i;
                }
            }
            throw new Exception("Can't find char in string.");
        }

        private static string[] cut(string str, int index)
        {
            int strLen = str.Length;
            StringBuilder untill = new StringBuilder();
            StringBuilder from = new StringBuilder();
            for (int i = 0; i < strLen; i++)
            {
                if (i < index)
                {
                    untill.Append(str[i]);
                }
                if (i > index)
                {
                    from.Append(str[i]);
                }
            }
            return new string[] { untill.ToString(), from.ToString() };
        }

        private static string[] cutFindLast(string str, char ch, int charth)
        {
            return cut(str, find(str, ch)[charth]);
        }

        private static string[] cutFindFirst(string str, char ch)
        {
            return cut(str, findFirst(str, ch));
        }

        private static string[] cutFindLast(string str, char ch)
        {
            return cut(str, findLast(str, ch));
        }
    }

    public struct Chapter
    {
        public string id;
        public string text;
        public IdPointer[] pointers;
    }

    public struct IdPointer
    {
        public string input;
        public string id;
    }
}
