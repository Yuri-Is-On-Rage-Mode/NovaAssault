using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace cali.Utils
{
    using System;
    using System.Collections.Generic;

    public class InputUtils
    {
        public static int WhatTypeIsThis(string thing)
        {
            if (thing == null) return 696969;

            // Check for path-like or special character indicators
            if (thing.Contains("/") || thing.Contains("\\") ||
                thing.Contains("@") || thing.Contains("$"))
            {
                return ProcessLenOfThis(thing);
            }

            return 0; // Default case if none of the conditions are met
        }

        public static string FurtherProcessThisPlease(string text)
        {
            return PleaseShortenThis(text);
        }

        public static int ProcessLenOfThis(string thing)
        {
            // Return 1 if the string length is 9 or greater
            return thing.Length >= 9 ? 1 : 0;
        }

        public static string PleaseShortenThis(string thing)
        {
            if (thing.Length < 999) return thing; // Return original if too short

            string shortenText = string.Concat(thing.Substring(0, 3));

            // Add ellipsis based on the length
            shortenText += new string('.', Math.Min(15, Math.Max(0, thing.Length - 7)));

            shortenText += thing.Substring(thing.Length - 4);

            return shortenText;
        }
    }

    internal class DesignFormat
    {
        public static void TakeInput(List<string> things)
        {
            if (things.Count < 2) return;

            Console.ForegroundColor = ConsoleColor.White;

            if (things.Count >= 4)
            {
                for (int i = 0; i < things.Count; i++)
                {
                    PrintStyledThing(things[i]);
                }
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    PrintStyledThing(things[i]);
                }
            }

            // Reset to default color after processing
            Console.ResetColor();
        }

        private static void PrintStyledThing(string thing)
        {
            int type = InputUtils.WhatTypeIsThis(thing);

            // Determine color based on type
            switch (type)
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case 1:
                    Console.ForegroundColor = ConsoleColor.Red;
                    thing = InputUtils.FurtherProcessThisPlease(thing);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            Console.Write(thing);
            Console.ForegroundColor = ConsoleColor.White; // Reset after printing
        }

        public static void Banner()
        {
            Console.WriteLine(@"
888888ba                              .d888888                                      dP   dP   ╔═══════════════════════════════╗
88    `8b                            d8'    88                                      88   88   ║ v-0.0.1 Beta-Prerelease       ║ 
88     88 .d8888b. dP   .dP .d8888b. 88aaaaa88a .d8888b. .d8888b. .d8888b. dP    dP 88 d8888P ║ Author: hmZa                  ║ 
88     88 88'  `88 88   d8' 88'  `88 88     88  Y8ooooo. Y8ooooo. 88'  `88 88    88 88   88   ╚═══════════════════════════════╝
88     88 88.  .88 88 .88'  88.  .88 88     88        88       88 88.  .88 88.  .88 88   88   
dP     dP `88888P' 8888P'   `88888P8 88     88  `88888P' `88888P' `88888P8 `88888P' dP   dP   
                                                                                              
                                   
╔═════════════════════════════════════════════════════════════════════════╗
║ Website: https://yuri-is-on-rage-mode.github.io/ref/hmza/nas/index.html ║ 
║ Github: https://github.com/Yuri-Is-On-Rage-Mode                         ║
╚═════════════════════════════════════════════════════════════════════════╝

");

        }

    }
}
