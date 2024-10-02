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

                Console.ForegroundColor = ConsoleColor.White;
            }
            public static void err(string err, string at)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($" ├───");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"> ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(err + "\n");

                Console.ForegroundColor = ConsoleColor.White;
            }
            public static void errlast(string err, string at)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($" └───");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"> ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(err + "\n");

                Console.ForegroundColor = ConsoleColor.White;
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

                Console.ForegroundColor = ConsoleColor.White;
            }
            public static void warn(string err, string at)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($" ├───");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"> ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(err + "\n");

                Console.ForegroundColor = ConsoleColor.White;
            }
            public static void warnlast(string err, string at)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($" └───");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"> ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(err + "\n");

                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public static void stdout(string warning)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(warning + "\n");

            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void stdoutfailed(string warning)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(warning + "\n");

            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void result(string warning)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("[result] ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(warning + "\n");
        }
        public class ModOut
        {
            public static void ListModules(List<cali_vm.Command.dmy.modules.Modules> Modules)
            {
                foreach (var Module in Modules)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"\n{Module.Get("name")} ");

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write($"({Module.Get("pubby")})");
                    Console.BackgroundColor = ConsoleColor.Black;

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($" [{Module.Get("pubon")}] ");

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($" {Module.Get("desc")}");
                }
                Console.WriteLine();
            }
        }
    }
}
