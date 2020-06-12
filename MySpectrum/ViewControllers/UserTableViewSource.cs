using System;
using System.Globalization;
using Foundation;
using MySpectrum.BusinessLogic.ViewModels;
using MySpectrum.Views;
using UIKit;

namespace MySpectrum.ViewControllers
{
    public class UserTableViewSource: UITableViewSource
    {
        private UserViewModel userViewModel;
        private const string UserCellName = "UserCell";

        public UserTableViewSource(ViewModelLocator Locator)
        {
            userViewModel = Locator.UserVM;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var userCell = tableView.DequeueReusableCell(UserCellName, indexPath) as UserInfo;
            var userData = userViewModel.Users[indexPath.Row];
            userCell.InitializeCell(userData);
            return userCell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
           return userViewModel.UserCount;
         
        }

        
    }
}
