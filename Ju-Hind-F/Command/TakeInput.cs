﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cali.Command
{
    internal class TakeInput
    {
    }

    public class UserInput 
    {
        public static string Input()
        { 
            string input = Console.ReadLine();
            return input;
        }

        public static List<string> Prepare(string input)
        {
            List<string> result = input.Split(" ").ToList();
            return result;
        }

    }
}
