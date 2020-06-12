using System;
using Foundation;
using Model = MySpectrum.BusinessLogic.Entities;
using MySpectrum.Utils;
using UIKit;
using MySpectrum.BusinessLogic.Utils;

namespace MySpectrum.Views
{
    public partial class UserInfo : UITableViewCell
    {
        public static readonly NSString Key = new NSString("UserInfo");
        public static readonly UINib Nib;

        static UserInfo()
        {
            Nib = UINib.FromName("UserInfo", NSBundle.MainBundle);
        }

        protected UserInfo(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }


        #region Methods

        public void InitializeCell(Model.User userInfo)
        {
            lblUserName.Text = userInfo.Name;
            lblAddress.Text = HelperMethods.FormatAddress(userInfo.Address);
            lblPhone.Text = userInfo.Phone;

            lblAddress.TextColor = IOSColors.GreyTextColor;
            lblPhone.TextColor = IOSColors.GreyTextColor;
            lblUserName.TextColor = IOSColors.HeaderBackgroundColor;

            ContentView.BackgroundColor = IOSColors.BodyBackgroundColor;
            SetViewBorder();
        }


        public void SetViewBorder()
        {
            MainView.Layer.BorderWidth = IOSConstants.ViewBorderWidthSingle;
            MainView.Layer.CornerRadius = IOSConstants.ViewCornerRadius;
            MainView.Layer.BorderColor = IOSColors.ViewGrayBorderColor;
        }
        #endregion
    }
}
