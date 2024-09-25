using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cali.Command.env.shel_scripting_lang.ByteCode;
using cali.Utils;

namespace cali.Command.env.shel_scripting_lang
{
    internal class ShellCompiler
    {
        public static void IdentifyTokens(string code)
        {
            //spilting the lines
            // List<string> Tasks = SeperateThemCommands(code.Split(";").ToList());


            //tokenizing the tasks
            // Tokenizer.From(Tasks);

            string codes = "";

            foreach (string task in code.Split(";").ToList())
            {
                foreach (string token in code.Split(" ").ToList())
                {
                    Console.ForegroundColor = ConsoleColor.White;

                    Dictionary<int, string> TokD = new Dictionary<int, string>();

                    TokD = Tokenizer.From(token);

                    foreach (var ByteCode in TokD)
                    {

                        Console.WriteLine($"$> {ByteCode.Value}");

                        codes = codes + ByteCode.Value;
                    }
                }
            }


            ByteCodeVM.RunCode.From(codes);
        }
    }

    public class Tokenizer
    {
        public static string Gap = "";
        public static Dictionary<int, string> From(string token)
        {

            Dictionary<int, string> TokenDict = new Dictionary<int, string>();
            int STATEMENT_COUNT = 0;

            //Console.WriteLine(token.GetType());

            #region headings
            if (token.StartsWith("RET") || token.StartsWith("DO"))
            {
                if (token.Equals("RET"))
                {
                    TokenDict[TokenDict.Count + 1] = Gap + "RET";
                    Gap = Gap + " ";
                }
                else if (token.Equals("DO"))
                {
                    TokenDict[TokenDict.Count + 1] = Gap + "DO";
                    Gap = Gap + " ";
                }
                else
                {
                    TokErrors.PleaseSeperateThem($"{token}", $"Code: [ {token} ]: Please seperate the `{token}` identifier and other stuff!");
                }
            }
            #endregion

            #region closeing_headings
            if (token.StartsWith("RN") || token.StartsWith("END"))
            {
                if (token.Equals("RN"))
                {
                    string NewGap = "";
                    for (int i = 0; i < Gap.Length - 1; i++)
                    {
                        NewGap = NewGap + " ";
                    }

                    Gap = NewGap;

                    TokenDict[TokenDict.Count + 1] = Gap + "RN";

                }
                else if (token.Equals("END"))
                {
                    string NewGap = "";
                    for (int i = 0; i < Gap.Length - 1; i++)
                    {
                        NewGap = NewGap + " ";
                    }

                    Gap = NewGap;

                    TokenDict[TokenDict.Count + 1] = Gap + "END";

                }
                else
                {
                    TokErrors.PleaseSeperateThem($"{token}", $"Code: [ {token} ]: Please seperate the `{token}` identifier and other stuff!");
                }
            }
            #endregion

            //ENDS WITH

            else if (token.Equals("") || token == "" || token == null || token == string.Empty)
            {
                //skip
            }
            else
            {
                //Console.WriteLine($"TOKEN: {token}\n\tTYPE: {token.GetType()}");
                TokenDict[TokenDict.Count + 1] = Gap + $"{token}";
            }



            return TokenDict;
        }

        class TokErrors
        {

            public static void PleaseSeperateThem(string tok, string msg)
            {

                errs.New($"Tok: `{tok}`: {msg}");
                errs.ListThemAll();

            }

        }

    }
}
