using Foundation;
using MySpectrum.BusinessLogic.Entities;
using MySpectrum.Utils;
using MySpectrum.Utils.CustomEventArgs;
using MySpectrum.ViewControllers;
using System;
using UIKit;
using MySpectrum.BusinessLogic.Utils;

namespace MySpectrum
{
    public partial class AddNewUserViewController : BaseViewController
    {
        public EventHandler CloseViewHandler;
        public event EventHandler<UserInfoEventArgs> AddUserHandler;
        private NSObject textChangedEventObserver;

        public AddNewUserViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
           

            this.txtName.ShouldReturn += TextFieldShouldReturn;
            this.txtPhone.ShouldReturn += TextFieldShouldReturn;
            this.txtAddressLine1.ShouldReturn += TextFieldShouldReturn;
            this.txtStreetLine2.ShouldReturn += TextFieldShouldReturn;
            this.txtCity.ShouldReturn += TextFieldShouldReturn;
            this.txtZipcode.ShouldReturn += TextFieldShouldReturn;
            this.txtPassword.ShouldReturn += TextFieldShouldReturn;

            txtPassword.ReturnKeyType = UIReturnKeyType.Done;

            txtZipcode.KeyboardType = UIKeyboardType.NumberPad;
            txtPhone.KeyboardType = UIKeyboardType.NumberPad;

            lblPasswordError.Hidden = true;

            txtZipcode.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                var newLength = textField.Text.Length + replacementString.Length - range.Length;
                return newLength <= IOSConstants.ZipCodeLength;
            };

            //Length restriction on home phonenumber
            txtPhone.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                var newLength = textField.Text.Length + replacementString.Length - range.Length;
                return newLength <= IOSConstants.PhoneLength;
            };

            //Length restriction on home phonenumber
            txtPassword.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                var newLength = textField.Text.Length + replacementString.Length - range.Length;
                return newLength <= IOSConstants.MaxPasswordLength;
            };

            ActiveScrollView = mainScrollView;

            //Handle the text changed event on textbox
            textChangedEventObserver = NSNotificationCenter.DefaultCenter.AddObserver(UITextField.TextFieldTextDidChangeNotification, TextChangedEvent);

        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            btnCancel.TouchUpInside += BtnCancel_TouchUpInside;
            AddUserBtn.TouchUpInside += AddUserBtn_TouchUpInside;
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);

            this.txtName.ShouldReturn += null;
            this.txtPhone.ShouldReturn += null;
            this.txtAddressLine1.ShouldReturn += null;
            this.txtStreetLine2.ShouldReturn += null;
            this.txtCity.ShouldReturn += null;
            this.txtZipcode.ShouldReturn += null;
            this.txtPassword.ShouldReturn += null;

            // Remove the text changed event observer
            if (textChangedEventObserver != null)
            {
                NSNotificationCenter.DefaultCenter.RemoveObserver(textChangedEventObserver);
            }
        }

        private void TextChangedEvent(NSNotification notification)
        {
            UITextField field = (UITextField)notification.Object;

            if (notification.Object == txtPhone)
            {
                string homePhone = txtPhone.Text;
                HelperMethods.FormatHomePhone(ref homePhone);
                txtPhone.Text = homePhone;
            }
            //else if(notification.Object == txtPassword)
            //{
            //    bool isValid = HelperMethods.PasswordValidate(txtPassword.Text);

            //    if(!isValid)
            //         lblPasswordError.Hidden = false;
            //    else
            //        lblPasswordError.Hidden = true;
            //}
        }

        private void AddUserBtn_TouchUpInside(object sender, EventArgs e)
        {
            AddUser(sender,e);
        }

        private void BtnCancel_TouchUpInside(object sender, EventArgs e)
        {
            if (CloseViewHandler != null)
            {
                CloseViewHandler(sender, null);
            }
        }

        private void AddUser(object sender, EventArgs e)
        {
      
            if (AddUserHandler != null && IsDataValid())
            {
                User newUser = new User();
                newUser.Name = txtName.Text;
                newUser.Phone = txtPhone.Text;
                newUser.Address = new Address();
                newUser.Address.LineOne = txtAddressLine1.Text;
                newUser.Address.LineTwo = txtStreetLine2.Text;
                newUser.Address.CityAndState = txtCity.Text;
                if(txtZipcode.Text != null && !String.IsNullOrWhiteSpace(txtZipcode.Text))
                    newUser.Address.Zip = int.Parse(txtZipcode.Text);
                AddUserHandler(sender, new UserInfoEventArgs() { UserInfo = newUser });
            }
        }


        private bool IsDataValid()
        {
            return IsPasswordValid();
        }

        private bool IsPasswordValid()
        {
            bool isValid = Validator.PasswordValidate(txtPassword.Text);

            if (!isValid)
            {
                lblPasswordError.Hidden = false;
                txtPassword.Layer.BorderColor = IOSColors.RedColor.CGColor;
                txtPassword.Layer.BorderWidth = 2;
                return false;
            }

            else
            {
                lblPasswordError.Hidden = true;
                txtPassword.Layer.BorderColor = IOSColors.GreyTextColor.CGColor;
                return true;
            }
        }
    }
}