using textGameEngine;

string[] file = fileOpener.openFile(true);
if (file.Length > 0)
{
    Chapter[] book = Compiler.compile(file, true);
    Console.WriteLine("Press Enter to start.");
    Console.ReadLine();
    Console.Clear();
    gameRunner.runGame(book);
}