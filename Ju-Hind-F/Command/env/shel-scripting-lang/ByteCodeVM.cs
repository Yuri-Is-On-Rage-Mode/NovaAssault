using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using cali.Utils;

namespace cali.Command.env.shel_scripting_lang.ByteCode
{
    internal class ByteCodeVM
    {
        public class RunCode
        {
            public static void From(string codes)
            {
                List<string> codeBytes = codes.Split(' ').ToList();
                VirtualMachine.StartRunningTheCodes(codeBytes);
            }
        }

        public class VirtualMachine
        {
            public static bool IsCodeBlocked = false;
            public static bool IsCodeComplete = true;
            public static bool IsCodeDangerous = false;

            public static void StartRunningTheCodes(List<string> code)
            {
                if (code != null)
                {
                    IsCodeBlocked = Checking.IsThisCodeBlocked(code);
                    IsCodeComplete = Checking.IsThisCodeComplete(code);
                    IsCodeDangerous = Checking.IsThisCodeDangerous(code);

                    if (IsCodeBlocked || !IsCodeComplete || IsCodeDangerous)
                    {
                        Console.WriteLine("(*) ByteCodeVM: Status");
                        DisplayStatus(IsCodeBlocked, "IsCodeBlocked");
                        DisplayStatus(!IsCodeComplete, "IsCodeComplete");
                        DisplayStatus(IsCodeDangerous, "IsCodeDangerous");

                        errs.ListThem();
                    }
                    else
                    {
                        Console.WriteLine("\n[+] RUNNING VM.... ..");
                        Process.ByteCode.Please(code);
                    }
                }
                else
                {
                    errs.New("UserDefined: Code: Is Empty!");
                    errs.ListThem();
                }
            }

            private static void DisplayStatus(bool condition, string message)
            {
                Console.Write($"      {message}:  ");
                Console.ForegroundColor = condition ? ConsoleColor.Red : ConsoleColor.Green;
                Console.WriteLine(condition);
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            public class Checking
            {
                public static bool IsThisCodeBlocked(List<string> codes)
                {
                    return codes.Contains("@BLOCKED");
                }

                public static bool IsThisCodeComplete(List<string> codes)
                {
                    return !codes.Contains("@NOT_COMPLETE");
                }

                public static bool IsThisCodeDangerous(List<string> codes)
                {
                    return codes.Contains("@DANGEROUS");
                }
            }
        }

        public class Process
        {
            public class ByteCode
            {
                public static long MaxLen = 68_719_476_736;
                public static List<List<string>> DpCodes = new List<List<string>>();

                public static void Please(List<string> codes)
                {
                    int codeLen = codes.Count;

                    if (codeLen >= MaxLen)
                    {
                        errs.New($"VM: Message: Your code is bigger than expected!\n\tYour Code Length: {codeLen}\n\tMax Code Process Length: {MaxLen}");
                        errs.ListThem();
                    }
                    else
                    {
                        warns.New("ByteCode Virtual Machine: May cause some errors!");
                        warns.ListThem();
                        warns.CacheClean();
                        Console.ForegroundColor = ConsoleColor.White;

                        SimpleShellInterpreter interpreter = new SimpleShellInterpreter();
                        interpreter.Run(string.Join(" ", codes));
                    }
                }

                public class Helper
                {
                    public static List<List<string>> SeparateStatements(List<string> codes)
                    {
                        List<List<string>> statements = new List<List<string>>();
                        List<string> currentStatement = new List<string>();

                        int nestedLevel = 0;

                        foreach (string code in codes)
                        {
                            if (code.ToUpper() == "DO")
                            {
                                nestedLevel++;
                            }
                            else if (code.ToUpper() == "RN")
                            {
                                nestedLevel--;
                                if (nestedLevel == 0)
                                {
                                    statements.Add(currentStatement.ToList());
                                    currentStatement.Clear();
                                }
                            }
                            else
                            {
                                currentStatement.Add(code);
                            }
                        }

                        return statements;
                    }
                }
            }
        }

        private class SimpleShellInterpreter
        {
            private Dictionary<string, object> variables = new Dictionary<string, object>();
            private Dictionary<string, Function> functions = new Dictionary<string, Function>();

            private class Function
            {
                public List<string> Parameters { get; set; }
                public List<string> Body { get; set; }
            }

            public List<string> Tokenize(string code)
            {
                return Regex.Matches(code, @"\S+|\n")
                            .Cast<Match>()
                            .Select(m => m.Value)
                            .ToList();
            }

            public void ParseAndExecute(List<string> tokens)
            {
                for (int i = 0; i < tokens.Count; i++)
                {
                    switch (tokens[i])
                    {
                        case RegisteredTokens.STMT_START:
                            i = ExecuteBlock(tokens, i + 1);
                            break;
                        case RegisteredTokens.FUNC:
                            i = DefineFunction(tokens, i + 1);
                            break;
                        case RegisteredTokens.CALL:
                            i = CallFunction(tokens, i + 1);
                            break;
                        case RegisteredTokens.VAR:
                            i = AssignVariable(tokens, i + 1);
                            break;
                    }
                }
            }

            private int ExecuteBlock(List<string> tokens, int start)
            {
                for (int i = start; i < tokens.Count && tokens[i] != RegisteredTokens.STMT_END; i++)
                {
                    if (tokens[i] == RegisteredTokens.STMT)
                    {
                        i = ExecuteStatement(tokens, i + 1);
                    }
                }
                return tokens.IndexOf(RegisteredTokens.STMT_END, start);
            }

            private int ExecuteStatement(List<string> tokens, int start)
            {
                string operation = tokens[start];
                if (new[] { RegisteredTokens.ADD, RegisteredTokens.SUBT, RegisteredTokens.MULT, RegisteredTokens.DIVI, RegisteredTokens.POWE, RegisteredTokens.MODU }.Contains(operation))
                {
                    return ArithmeticOperation(tokens, start);
                }
                return start;
            }

            private int ArithmeticOperation(List<string> tokens, int start)
            {
                string operation = tokens[start];
                List<double> values = new List<double>();
                int i = start + 1;
                for (; i < tokens.Count && tokens[i] != RegisteredTokens.RET; i++)
                {
                    if (variables.TryGetValue(tokens[i], out object value))
                    {
                        values.Add(Convert.ToDouble(value));
                    }
                    else if (double.TryParse(tokens[i], out double numValue))
                    {
                        values.Add(numValue);
                    }
                    else
                    {
                        throw new ArgumentException($"Invalid value or undefined variable: {tokens[i]}");
                    }
                }

                double result = values[0];
                for (int j = 1; j < values.Count; j++)
                {
                    switch (operation)
                    {
                        case RegisteredTokens.ADD:
                            result += values[j];
                            break;
                        case RegisteredTokens.SUBT:
                            result -= values[j];
                            break;
                        case RegisteredTokens.MULT:
                            result *= values[j];
                            break;
                        case RegisteredTokens.DIVI:
                            if (values[j] == 0)
                                throw new DivideByZeroException("Division by zero is not allowed.");
                            result /= values[j];
                            break;
                        case RegisteredTokens.POWE:
                            result = Math.Pow(result, values[j]);
                            break;
                        case RegisteredTokens.MODU:
                            if (values[j] == 0)
                                throw new DivideByZeroException("Modulo by zero is not allowed.");
                            result %= values[j];
                            break;
                    }
                }

                if (i < tokens.Count && tokens[i] == RegisteredTokens.RET)
                {
                    i = HandleReturn(tokens, i + 1, result);
                }

                return i;
            }


            private int HandleReturn(List<string> tokens, int start, double value)
            {
                if (tokens[start] == RegisteredTokens.RET_AS)
                {
                    string typeCast = tokens[start + 1];
                    string varName = tokens[start + 3].Trim('(', ')');
                    switch (typeCast)
                    {
                        case RegisteredTokens.RET_AS_FLOAT:
                            variables[varName] = (float)value;
                            break;
                        case RegisteredTokens.RET_AS_INT:
                        case RegisteredTokens.RET_AS_LONG:
                            variables[varName] = (long)value;
                            break;
                    }
                    return start + 4;
                }
                return start;
            }

            private int DefineFunction(List<string> tokens, int start)
            {
                string funcName = tokens[start];
                List<string> parameters = tokens[start + 1].Split(',').ToList();
                int bodyStart = start + 2;
                int bodyEnd = tokens.IndexOf(RegisteredTokens.STMT_END, bodyStart);
                functions[funcName] = new Function
                {
                    Parameters = parameters,
                    Body = tokens.GetRange(bodyStart, bodyEnd - bodyStart)
                };
                return bodyEnd;
            }


            private int CallFunction(List<string> tokens, int start)
            {
                string funcName = tokens[start];
                Dictionary<string, object> args = new Dictionary<string, object>();
                int i = start + 1;
                for (; i < tokens.Count && tokens[i] != RegisteredTokens.RET; i++)
                {
                    if (tokens[i].Contains('='))
                    {
                        string[] parts = tokens[i].Split('=');
                        string argName = parts[0];
                        string argValue = parts[1].Trim('(', ')');
                        if (double.TryParse(argValue, out double numValue))
                        {
                            args[argName] = numValue;
                        }
                        else if (variables.TryGetValue(argValue, out object varValue))
                        {
                            args[argName] = varValue;
                        }
                        else
                        {
                            throw new ArgumentException($"Invalid argument value: {argValue}");
                        }
                    }
                }

                if (functions.TryGetValue(funcName, out Function func))
                {
                    for (int j = 0; j < func.Parameters.Count; j++)
                    {
                        string paramName = func.Parameters[j].TrimStart('!');
                        if (args.TryGetValue(paramName, out object argValue))
                        {
                            variables[paramName] = argValue;
                        }
                        else if (variables.TryGetValue(paramName, out object globalValue))
                        {
                            // Use the global value if it exists and no argument was provided
                            variables[paramName] = globalValue;
                        }
                        else
                        {
                            throw new ArgumentException($"Missing argument for parameter: {paramName}");
                        }
                    }
                    ParseAndExecute(func.Body);
                }
                else
                {
                    throw new ArgumentException($"Undefined function: {funcName}");
                }

                return i;
            }


            private int AssignVariable(List<string> tokens, int start)
            {
                string varName = tokens[start];
                if (tokens[start + 1] == RegisteredTokens.IS)
                {
                    object value = EvaluateExpression(tokens.GetRange(start + 2, tokens.Count - start - 2));
                    variables[varName] = value;
                }
                return tokens.IndexOf(RegisteredTokens.STMT_END, start);
            }

            private object EvaluateExpression(List<string> tokens)
            {
                if (tokens[0] == RegisteredTokens.STMT)
                {
                    return ExecuteStatement(tokens, 1);
                }
                return tokens[0];
            }

            private object EvaluateExpression(string expression)
            {
                // This is a simplified version. You might want to implement a more robust expression evaluator.
                return double.Parse(expression);
            }

            public void Run(string code)
            {
                try
                {
                    List<string> tokens = Tokenize(code);
                    ParseAndExecute(tokens);
                    Console.WriteLine("Variables after execution:");
                    foreach (var variable in variables)
                    {
                        Console.WriteLine($"{variable.Key}: {variable.Value}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during execution: {ex.Message}");
                }
            }
        }
    }

    public class RegisteredTokens
    {
        public const string STMT_START = "DO";
        public const string STMT_END = "RN";
        public const string STMT = "STMT";
        public const string ADD = "ADD";
        public const string SUBT = "SUBT";
        public const string MULT = "MULT";
        public const string DIVI = "DIVI";
        public const string POWE = "POWE";
        public const string MODU = "MODU";
        public const string FUNC = "FUNC";
        public const string VAR = "VAR";
        public const string IS = "IS";
        public const string RET = "RET";
        public const string RET_AS = "AS";
        public const string RET_AS_FLOAT = "FLOAT";
        public const string RET_AS_INT = "INT";
        public const string RET_AS_STRING = "STRING";
        public const string RET_AS_LONG = "LONG";
        public const string RET_AS_DOUBLE = "DOUBLE";
        public const string RET_AS_LIST = "LIST";
        public const string RET_AS_ARRAY = "ARRAY";
        public const string CALL = "CALL";
        public const string SHOW = "DISPLAY";
        public const string DISPLAY_FORMAT = "FORMAT";
        public const string ENV = "SEDO";
        public const string ASM = "CASM";
    }
}