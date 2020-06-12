using GalaSoft.MvvmLight.Ioc;
namespace MySpectrum.BusinessLogic.ViewModels
{
    public class ViewModelLocator
    {
        #region Constructor
        static ViewModelLocator()
        {
            SimpleIoc.Default.Register<UserViewModel>();
          
        }

        #endregion

        #region Property

        /// <summary>
        /// Gets the user
        /// </summary>
        /// <value>The user object.</value>
        public UserViewModel UserVM
        {
            get
            {
                return SimpleIoc.Default.GetInstance<UserViewModel>();
            }
        }


       

        #endregion


    }
}
