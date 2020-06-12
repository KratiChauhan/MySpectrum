using System;
using System.Text.RegularExpressions;

namespace MySpectrum.BusinessLogic.Utils
{
    public static class Validator
    {

        private static readonly int MinPasswordLength = 5;

        public static bool PasswordValidate(string data)
        {

            if (data.Length < MinPasswordLength)
                return false;

            if (isRepeat(data))
                return false;

            return Regex.IsMatch(data, @"^(((?=.*[a-z])(?=.*[A-Z]))|((?=.*[a-z])(?=.*[0-9]))|((?=.*[A-Z])(?=.*[0-9])))(?=.{6,})");
        }



        static bool isRepeat(String str)
        {
            int n = str.Length;
            int[] lps = new int[n];

            computeLPSArray(str, n, lps);

            int len = lps[n - 1];

            return (len > 0 && n % (n - len) == 0)
                                   ? true : false;
        }

        static void computeLPSArray(String str, int M, int[] lps)
        {
            int len = 0;
            int i;

            lps[0] = 0; 
            i = 1;
   
            while (i < M)
            {
                if (str[i] == str[len])
                {
                    len++;
                    lps[i] = len;
                    i++;
                }
                else 
                {
                    if (len != 0)
                    {
                        len = lps[len - 1];
                    }
                    else 
                    {
                        lps[i] = 0;
                        i++;
                    }
                }
            }
        }
    }
}
