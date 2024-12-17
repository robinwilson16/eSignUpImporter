using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSignUpImporter.Shared
{
    public static class StringFunctions
    {
        public static string? SubstringOrDefault(this string? value, int? startIndex)
        {
            string? result;
            if (startIndex == null)
            {
                result = value;
            }
            else
            {
                result = value?.Substring((int)startIndex);
            }
            
            return result;
        }

        public static string? SubstringOrDefault(this string? value, int? startIndex, int? length)
        {
            string? result;
            if (startIndex == null)
            {
                result = null;
            }
            else if(length == null)
            {
                result = value?.Substring((int)startIndex);
            }
            if(value?.Length > length - startIndex)
            {
                result = value?.Substring((int)startIndex!, (int)length!);
            }
            else
            {
                result = value;
            }

            return result;
        }
    }
}
