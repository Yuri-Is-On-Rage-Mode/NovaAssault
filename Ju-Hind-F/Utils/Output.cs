using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cali.Utils
{
    internal class caliOutput
    {
        public class erroroutputs
        {
            public static void errinfo(string err)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("[E] ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(err + "\n");
            }
            public static void err(string err, string at)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($" ├───");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"> ");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(err + "\n");
            }
            public static void errlast(string err, string at)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($" └───");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"> ");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(err + "\n");
            }
        }

        public class warningoutputs
        {
            public static void warninfo(string err)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("[W] ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(err + "\n");
            }
            public static void warn(string err, string at)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($" ├───");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"> ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(err + "\n");
            }
            public static void warnlast(string err, string at)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($" └───");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"> ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(err + "\n");
            }
        }
        public static void stdout(string warning)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[info] ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(warning + "\n");
        }
        public static void result(string warning)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("[result] ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(warning + "\n");
        }
    }
}
