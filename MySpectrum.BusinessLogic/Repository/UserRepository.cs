using System;
using System.Collections.Generic;
using MySpectrum.BusinessLogic.Entities;

namespace MySpectrum.BusinessLogic.Repository
{
    public static class UserRepository
    {
        public static List<User> Users { get; set; }
        static UserRepository()
        {
           GetUsers();
        }

        private static void GetUsers()
        {
           
            Users = new List<User>();

            Users.Add(new User()
            {
                ID = "1",
                Name = "Jean Clancy",
                Phone = "(234) 432-2312",
                Address = new Address()
                {
                    LineOne = "1234 Apt 123",
                    LineTwo = "Hills View",
                    CityAndState = "San Diego, CA",
                    Zip = 92101
                }

            });

            Users.Add(new User()
            {
                ID = "1",
                Name = "Nisha Kumar",
                Phone = "(234) 432-2312",
                Address = new Address()
                {
                    LineOne = "1234 Apt 123",
                    LineTwo = "Glassglow Park",
                    CityAndState = "San Diego, CA",
                    Zip = 92101
                }

            });
           
        }
  }
}
