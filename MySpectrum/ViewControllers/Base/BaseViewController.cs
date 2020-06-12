using System;
using System.Collections.Generic;
using Foundation;
using GalaSoft.MvvmLight.Views;
using MySpectrum.BusinessLogic.ViewModels;
using UIKit;

namespace MySpectrum.ViewControllers
{
    public class BaseViewController :UIViewController
    {
        public BaseViewController(IntPtr handle) : base(handle)
        {
        }

        private NSObject didShowNotificationObserver;
        private NSObject willHideNotificationObserver;
        private nfloat keyboradBottomPadding = 10;
        private nfloat ViewHeight;
        public UITextField ActiveTextField { get; set; }
        public UIScrollView ActiveScrollView { get; set; }
        private NSObject textFieldBeginEditingEventObserve;

        private static ViewModelLocator _locator;

        public static ViewModelLocator Locator
        {
            get
            {
                _locator = _locator ?? (_locator = new ViewModelLocator());
                return _locator;
            }
        }

        public nfloat KeyBoardHeightWithPadding { get; set; }


        protected bool TextFieldShouldReturn(UITextField textfield)
        {
            //Check if textfield is null
            if (textfield != null)
            {
                nint nextTag = textfield.Tag + 1;
                UIResponder nextResponder = this.View.ViewWithTag(nextTag);
                if (nextResponder != null && nextResponder.CanBecomeFirstResponder)
                {
                    nextResponder.BecomeFirstResponder();
                }
                else
                {
                    // Not found, so remove keyboard.
                    textfield.ResignFirstResponder();
                }
            }
            return false; // We do not want UITextField to insert line-br
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ViewHeight = View.Frame.Height;
            textFieldBeginEditingEventObserve = NSNotificationCenter.DefaultCenter.AddObserver(UITextField.TextDidBeginEditingNotification, TextFieldBeginEditingChangedEvent);

        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            //Handles keyboard appearance
            didShowNotificationObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.DidShowNotification, KeyBoardUpNotification);

            //Handles keyboard disappearance
            willHideNotificationObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, KeyBoardDownNotification);


        }

        protected void KeyBoardUpNotification(NSNotification notification)
        {
            CoreGraphics.CGRect notificationBounds = UIKeyboard.BoundsFromNotification(notification);

            CoreGraphics.CGRect frame = View.Frame;
            frame.Height = ViewHeight - notificationBounds.Height - keyboradBottomPadding;
            this.KeyBoardHeightWithPadding = (notificationBounds.Height + keyboradBottomPadding);
            this.View.Frame = frame;
            this.View.LayoutIfNeeded();

            if (ActiveTextField != null && ActiveScrollView != null)
            {
                ActiveScrollView.ScrollEnabled = true;
                this.ActiveScrollView.ScrollRectToVisible(ActiveTextField.Frame, true);
            }
        }

        /// <summary>
        /// Keies the board down notification.
        /// </summary>
        /// <param name="notification">Notification.</param>
        protected void KeyBoardDownNotification(NSNotification notification)
        {
            CoreGraphics.CGRect notificationBounds = UIKeyboard.BoundsFromNotification(notification);
            CoreGraphics.CGRect frame = View.Frame;
            frame.Height = frame.Height + notificationBounds.Height + keyboradBottomPadding;
            this.View.Frame = frame;
        }

        private void TextFieldBeginEditingChangedEvent(NSNotification notification)
        {
            ActiveTextField = (UITextField)notification.Object;
        }


        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            //Dispose didShowNotificationObserver
            if (didShowNotificationObserver != null)
            {
                NSNotificationCenter.DefaultCenter.RemoveObserver(didShowNotificationObserver);
            }
            //Dispose willHideNotificationObserver
            if (willHideNotificationObserver != null)
            {
                NSNotificationCenter.DefaultCenter.RemoveObserver(willHideNotificationObserver);
            }

            //Dispose textFieldBeginEditingEventObserve
            if (textFieldBeginEditingEventObserve != null)
            {
                NSNotificationCenter.DefaultCenter.RemoveObserver(textFieldBeginEditingEventObserve);
            }

        }
    }
}
