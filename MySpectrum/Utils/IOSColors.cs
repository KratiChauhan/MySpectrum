using System;
using CoreGraphics;
using UIKit;

namespace MySpectrum.Utils
{
    public static class IOSColors
    {
        public static CGColor ViewGrayBorderColor { get { return ExtensionMethods.ToUIColor("6A6A6A").CGColor; } }
        public static UIColor BodyBackgroundColor { get { return ExtensionMethods.ToUIColor("C1DBF8"); } }
        public static UIColor HeaderBackgroundColor { get { return ExtensionMethods.ToUIColor("3D96F4"); } }
        public static UIColor GreyTextColor { get { return ExtensionMethods.ToUIColor("B5B7B9"); } }
        public static UIColor White { get { return ExtensionMethods.ToUIColor("#FFFFFF"); }}
        public static UIColor RedColor { get { return ExtensionMethods.ToUIColor("C1260A"); } }
    }
}
