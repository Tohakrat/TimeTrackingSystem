using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContracts
{
    public class User : BaseEntity
    {
        public User(string username, string password, AccessRole role,string fullName=null)
        {
            UserName = username;
            PassWord = password;
            Role = role;
            FullName=(fullName??null); 
        }
        private string UserName { get; set; }
        private string PassWord { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        private AccessRole Role { get; set; }

    }
}
