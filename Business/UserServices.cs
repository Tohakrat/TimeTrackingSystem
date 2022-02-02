﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContracts;

namespace Business
{
    public class UserServices
    {
        private List<User> UserRepository = new();
        private List<UserData> UserData = new List<UserData>();
        public List<User> GetAllUsers()
        {
            return UserRepository;
        }
        public List<User> GetAllActiveUsers()
        {
            IEnumerable<User> ActiveUsers = from U in UserRepository 
                                     where U.IsActive                                
                                select U; 
            /*List<User> ActiveUsers = new();
            foreach (User U in UserRepository)
            {
                if (U.IsActive)
                {
                    ActiveUsers.Add(U);
                }
            }*/
            return ActiveUsers.ToList();
        }
        public bool getUserByLogin(string userName, out User user)
        {
            //successfullyFinded = false;
            foreach (User U in UserRepository)
            {
                if (userName == U.UserName)
                {
                    user = U;
                    return true;
                }
            }
            user = null;
            return false;
        }
        public void Add(User user)
        {
            UserRepository.Add(user);
            UserData.Add(new UserData(user));
        }
        public void Delete(User user)
        {
            UserRepository.Add(user);
            UserData.Add(new UserData(user));
        }

    }
}
