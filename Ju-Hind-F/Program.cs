using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Diagnostics;
using System.Collections;

using cali.Utils;
using cali.Command;
using cali.Command.env;
//using cali.Tests;

namespace caliVirtualOS
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("(c) cali Initial Developers | Fri3nds .G");
            DesignFormat.Banner();

            Initcali();
        }

        public static void Initcali()
        {
            #region UnitTests
            //UnitTests.Test1();
            //UnitTests.Test2();
            //UnitTests.Test3();
            //UnitTests.Test4();
            //UnitTests.Test_RunCmd();

            //cali.Command.env.Vars.main_test();
            //UnitTests.Test_Exit();
            #endregion

            #region shall tests
            //abc:
            //List<string> parts= new List<string>();
            //parts = ["@bin","ls","--path", "$(BinPath)"];
            //for (int i = 0; i < parts.Count; i++)
            //{
            //    if (parts[i].StartsWith("$("))
            //    {
            //        parts[i] = parts[i].Replace("$(", "");
            //        parts[i] = parts[i].Replace(")", "");

            //        Console.WriteLine($":: {parts[i]}");
            //    }
            //    else
            //    {

            //    }
            //}
            //Console.ReadLine();
            //goto abc;
        #endregion

        #region Actual Init

        a:

            DesignFormat.TakeInput([$"\n{CommandEnv.CURRENT_USER_NAME}", "@", "cali-VM", ": ", $"{cali.Command.CommandEnv.CurrentDirDest}", " $ "]);

            List<string> commands = UserInput.Prepare(UserInput.Input());

            IdentifyCommand.Identify(commands);
            List<string> parsed_commands = IdentifyCommand.ReturnThemPlease();

            PleaseCommandEnv.TheseCommands(parsed_commands);

            IdentifyCommand.CacheClean();
            goto a;

            #endregion
        }

    }
}