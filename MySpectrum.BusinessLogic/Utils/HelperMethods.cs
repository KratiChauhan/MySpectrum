using System;
using System.Text;
using System.Text.RegularExpressions;
using MySpectrum.BusinessLogic.Entities;

namespace MySpectrum.BusinessLogic.Utils
{
    public static class HelperMethods
    {
        private static readonly string BracketStartSeparator = "(";
        private static readonly string BracketEndSeparator = ")";
        private static readonly string DashSeperator = "-";

        public static void FormatHomePhone(ref string homePhone)
        {
            if (!string.IsNullOrEmpty(homePhone))
            {
                if (homePhone.Length.Equals(1) && homePhone[0].ToString() != BracketStartSeparator)
                {
                    homePhone = homePhone.Insert(0, BracketStartSeparator);
                }
                else if (homePhone.Length.Equals(5) && homePhone[4].ToString() != BracketEndSeparator)
                {
                    homePhone = homePhone.Insert(4, BracketEndSeparator);
                    homePhone = homePhone.Insert(5, " ");
                }
                else if (homePhone.Length.Equals(10) && homePhone[9].ToString() != DashSeperator)
                {
                    homePhone = homePhone.Insert(9, DashSeperator);
                }
                else
                {
                    //Nothing has to be done
                }
            }
        }

        public static string FormatAddress(Address address)
        {
            StringBuilder addressString = new StringBuilder();

            if(address != null)
            {
                if(!string.IsNullOrWhiteSpace(address.LineOne))
                {
                    addressString.Append(address.LineOne);
                }
                if (!string.IsNullOrWhiteSpace(address.LineTwo))
                {
                    addressString.Append(", " + address.LineTwo);
                }
                if (!string.IsNullOrWhiteSpace(address.CityAndState))
                {
                    addressString.Append("\n" + address.CityAndState);
                }
                if (address.Zip != 0)
                {
                    addressString.Append(", " + address.Zip);
                }

            }

            return addressString.ToString();
        }


       
    }
}
