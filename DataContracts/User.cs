using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContracts
{
    public class User : BaseEntity
    {
        public User(string username, string password, AccessRole role)
        {
            UserName = username;
            PassWord = password;
            Role = role;
        }
        private string UserName { get; set; }
        private string PassWord { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        private AccessRole Role { get; set; }

    }
}
