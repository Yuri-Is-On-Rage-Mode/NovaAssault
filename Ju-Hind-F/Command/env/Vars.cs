using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

using cali.Command.env;
using cali.Command.env.shel_scripting_lang;
using cali.Command.env.shel_scripting_lang.ByteCode;

namespace cali.Command.env
{
    internal class Vars
    {
        public static void main_test()
        {
            //@BLOCKED      : TO MAKE CODE BLOCKED TO RUN ON VM
            //@NOT_COMPLETE : TO MAKE IT STOP , AND DONT RUN ON VM , BECAUSE IT IS NOT COMPLETE
            //@DANGEROUS    : TO MAKE CODE DANGEROUS AND BLOCKED BY VM

            //ShellCompiler.IdentifyTokens(@"RET DO ADD 123 456 789 100 RN");
            string codes = @"
DO PRAGMA ONCE
  DO VAR added_numbers IS FLOAT RN
  DO STMT ADD 123 456 789 RET AS FLOAT (added_numbers) RN
  FUNC a_new_function(!added_numbers , new_numbers) DO
    STMT ADD added_numbers new_numbers RET AS LONG (numbers_are_added_and_converted_to_long)
  RN
  CALL a_new_function new_numbers=(1200)
  DO VAR abc IS STMT POWE 2 3 RN
RN
";
            ByteCodeVM.RunCode.From(codes);
        }
    }
}
