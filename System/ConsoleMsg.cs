using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UproxiDOS.System
{
    public static class ConsoleMsg
    {
        public static void Error(string err)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ ERROR ]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(err + "\n");
        }

        public static void Warn(string warn)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[ WARN ]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(warn + "\n");
        }

        public static void Info(string info)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("[ INFO ]");
            Console.Write(info + "\n");
        }

        public static string CenterText(string text)
        {
            int consoleWidth = 90;
            int padding = (consoleWidth - text.Length) / 2;
            string centeredText = text.PadLeft(padding + text.Length).PadRight(consoleWidth);
            return centeredText;
        }
    }
}
