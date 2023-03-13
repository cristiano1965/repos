using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Packt.Shared; 
public class StringExtensions
{
    public static bool IsValidEmail(string input) 
    {
        // use simple regular expression to check
        // that input string is  email valid address
        return Regex.IsMatch(input,
            @"[a-zA-Z0-9\.-_]+@[a-zA-Z0-9\.-_]+"
            );
    }
}

