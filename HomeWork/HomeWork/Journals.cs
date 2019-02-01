using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork
{
    class Journals
    {
        public static void ConsoleJournal(string str, object obj)
        {
            if (obj is ConsoleColor)
            {
                Console.ForegroundColor = (ConsoleColor)obj;
            }
            Console.WriteLine($"{DateTime.Now} - {str}, кол-во очков: {Game.GamePoints}");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void FileJournal(string str, object obj)
        {
            String path = Directory.GetCurrentDirectory() + "\\save.txt";
            using (StreamWriter stringWriter = File.AppendText(path))
            {
                if (Game.NewGame)
                {
                    stringWriter.WriteLine($"-------------Начало новой игры !-----------");
                    Game.NewGame = false;
                }
                stringWriter.WriteLine($"{DateTime.Now} - {str}, кол-во очков: {Game.GamePoints}");
                stringWriter.Close();
            }
        }
    }
}
