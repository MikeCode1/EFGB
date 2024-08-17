using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using textGameEngine;

namespace textGameEngine
{
    internal static class gameRunner
    {
        private static Random random;
        public static void runGame(Chapter[] book)
        {
            if (book.Length > 0)
            {
                random = new Random();
                int page = 0;
                while (page >= 0)
                {
                    Console.WriteLine(book[page].text);
                    string input = Console.ReadLine().ToLower();
                    if (input.ToLower() == "esc" || input.ToLower() == "exit" || input.ToLower() == "quit" || input.ToLower() == "escape")
                    {
                        break;
                    }
                    if (book[page].pointers.Length > 0)
                    {
                        page = findChapter(book, findIdFromInput(book[page].id, input, book[page].pointers));
                    }
                    else
                    {
                        page++;
                    }
                }
            }
        }

        public static string findIdFromInput(string currentId, string input, IdPointer[] pointers)
        {
            List<string> ids = new List<string>();
            for (int i = 0; i < pointers.Length; i++)
            {
                if (input == pointers[i].input)
                {
                    ids.Add(pointers[i].id);
                }
            }
            string[] arrIds = ids.ToArray();
            int idA = arrIds.Length;
            if (idA > 0)
            {
                return arrIds[random.Next(arrIds.Length)];
            }
            else
            {
                return currentId;
            }
        }

        public static int findChapter(Chapter[] book, string id)
        {
            List<int> indexs = new List<int>();
            for (int i = 0; i < book.Length; i++)
            {
                if (book[i].id == id)
                {
                    indexs.Add(i);
                }
            }
            int[] arrIndexs = indexs.ToArray();
            int indexA = arrIndexs.Length;
            if (indexA > 0)
            {
                return arrIndexs[random.Next(arrIndexs.Length)];
            }
            else
            {
                return -1;
            }
        }
    }
}
