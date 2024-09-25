using System;
using System.Collections.Generic;

namespace cali.Utils
{
    internal class Errors
    {
        // No changes needed for this part yet
    }

    public class errs
    {
        // List to store error messages
        public static List<string> ErrorsAre = new List<string>();

        // Add a new error message to the list
        public static void New(string err_msg)
        {
            ErrorsAre.Add(err_msg);
        }

        // Clear the error message cache
        public static void CacheClean()
        {
            ErrorsAre.Clear();
        }

        // List errors, use errlast for the last error and .err for others
        public static void ListThem()
        {
            if (ErrorsAre.Count > 0)
            {
                if (ErrorsAre.Count <= 20)
                {
                    caliOutput.erroroutputs.errinfo($"Got {ErrorsAre.Count} Errors: Listing {ErrorsAre.Count} out of {ErrorsAre.Count} Errors!");

                    for (int i = 0; i < ErrorsAre.Count - 1; i++)
                    {
                        caliOutput.erroroutputs.err(ErrorsAre[i], "err"); // Regular errors
                    }

                    // Use errlast for the last error
                    errlast(ErrorsAre[^1]);
                }
                else
                {
                    caliOutput.erroroutputs.errinfo($"Got {ErrorsAre.Count} Errors: Listing 20 out of {ErrorsAre.Count} Errors!");

                    for (int i = 0; i < 19; i++) // List first 19 as regular errors
                    {
                        caliOutput.erroroutputs.err(ErrorsAre[i], "err");
                    }

                    // Use errlast for the 20th error
                    errlast(ErrorsAre[19]);
                }
            }
            else
            {
                caliOutput.warningoutputs.warninfo("0 Errors Found!");
            }
        }

        // List all errors and use errlast for the last error printed
        public static void ListThemAll()
        {
            if (ErrorsAre.Count > 0)
            {
                caliOutput.erroroutputs.errinfo($"Got {ErrorsAre.Count} Errors: Listing {ErrorsAre.Count} out of {ErrorsAre.Count} Errors!");

                for (int i = 0; i < ErrorsAre.Count - 1; i++) // Print all but the last error normally
                {
                    caliOutput.erroroutputs.err(ErrorsAre[i], "err");
                }

                // Use errlast for the last error
                errlast(ErrorsAre[^1]);
            }
            else
            {
                caliOutput.warningoutputs.warninfo("0 Errors Found!");
            }
        }

        // Print the last error using errlast
        public static void errlast(string lastError)
        {
            caliOutput.erroroutputs.errlast(lastError, "err");
        }
    }
}
