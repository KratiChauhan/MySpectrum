using System;
using MySpectrum.BusinessLogic.Entities;

namespace MySpectrum.Utils.CustomEventArgs
{
    public class UserInfoEventArgs : EventArgs
    {
       public User UserInfo { get; set; }
    }
}
