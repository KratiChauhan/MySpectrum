using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Command;
using MySpectrum.BusinessLogic.Entities;
using MySpectrum.BusinessLogic.Repository;

namespace MySpectrum.BusinessLogic.ViewModels
{
    public class UserViewModel
    {

        public UserViewModel()
        {
            Users = UserRepository.Users;

        }


        public List<User> Users { get; set; }

        public int UserCount
        {
            get
            {
                return Users.Count;
            }
        }


        public void AddUser(User newUser)
        {
            Users.Add(newUser);
        }

      

      


    }
}
