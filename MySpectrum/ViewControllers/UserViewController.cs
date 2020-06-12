using Foundation;
using MySpectrum.BusinessLogic.ViewModels;
using MySpectrum.Utils;
using MySpectrum.ViewControllers;
using MySpectrum.Views;
using System;
using UIKit;

namespace MySpectrum
{
    public partial class UserViewController : BaseViewController
    {
        private AddNewUserViewController addUserViewController;
        private const string HeaderCellName = "HeaderCell";
        private const string UserCellName = "UserCell";
        private UserViewModel userViewModel;

        public UserViewController (IntPtr handle) : base (handle)
        {
            userViewModel = Locator.UserVM;
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SetUserTableView();
            SetUIAppearance();

            AddUserButton.TouchUpInside += AddUserButton_TouchUpInside;
        }

        private void SetUIAppearance()
        {
            NavigationController.NavigationBarHidden = true;
            HeaderView.BackgroundColor = IOSColors.HeaderBackgroundColor;
            TitleLabel.TextColor = IOSColors.White;
            AddUserButton.TintColor = IOSColors.White;

            
        }

        private void SetUserTableView()
        {
            
            UserTableView.RegisterNibForCellReuse(UserInfo.Nib, UserCellName);

            UserTableView.AllowsSelection = false;
            UserTableView.RowHeight = UITableView.AutomaticDimension;
            UserTableView.EstimatedRowHeight = 200;
            UserTableView.BackgroundColor = IOSColors.BodyBackgroundColor;
        }
       
        private void AddUserButton_TouchUpInside(object sender, EventArgs e)
        {
            var mainStoryboard = UIStoryboard.FromName("Main", null);
            addUserViewController = mainStoryboard.InstantiateViewController("AddNewUserViewController") as AddNewUserViewController;
            addUserViewController.ModalPresentationStyle = UIModalPresentationStyle.CurrentContext;
            addUserViewController.CloseViewHandler += PopupviewController_CloseViewHandler;
            addUserViewController.AddUserHandler += AddUserViewController_AddUserHandler;
            NavigationController.PresentViewController(addUserViewController, true, null);

        }

        private void AddUserViewController_AddUserHandler(object sender, Utils.CustomEventArgs.UserInfoEventArgs e)
        {
            userViewModel.AddUser(e.UserInfo);
            UserTableView.ReloadData();
            if (addUserViewController != null)
            {
                addUserViewController.DismissViewController(true, null);
            }
        }

        void PopupviewController_CloseViewHandler(object sender, EventArgs e)
        {
            if (addUserViewController != null)
            {
                addUserViewController.DismissViewController(true, null);
            }
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            
            var tableViewSource = new UserTableViewSource(Locator);
            UserTableView.Source = tableViewSource;

            UserTableView.RowHeight = UITableView.AutomaticDimension;
            UserTableView.EstimatedRowHeight = 150;
            UserTableView.ReloadData();
        }
    }
}