using System;
using System.Globalization;
using UIKit;

namespace MySpectrum.Utils
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Convert hex string color to UIColor
        /// </summary>
        /// <returns>The UIColor.</returns>
        /// <param name="value">Hex string.</param>
        public static UIColor ToUIColor(this string value)
        {
            int red = 0;
            int green = 0;
            int blue = 0;
            //Check if hexString is not null
            if (!string.IsNullOrEmpty(value))
            {
                string replaceValue = value.Replace("#", "");

                if (replaceValue.Length == 3)
                {
                    replaceValue = replaceValue + replaceValue;
                }

                red = Int32.Parse(replaceValue.Substring(0, 2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
                green = Int32.Parse(replaceValue.Substring(2, 2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
                blue = Int32.Parse(replaceValue.Substring(4, 2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
            }
            return UIColor.FromRGB(red, green, blue);
        }
    }
}
