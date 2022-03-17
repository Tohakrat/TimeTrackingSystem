using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Infrastructure;
[assembly: InternalsVisibleTo("Business")]
namespace DataContracts
{
    [Serializable]
    public class User : BaseEntity
    {
        public User(int id,string username, string password, AccessRole role,string fullName=null)
        {
            Id = id;
            UserName = username;
            PassWord = password;
            Role = role;
            FullName=(fullName??null); 
        }
        public User()
        {
            Id = 0;
            UserName = "";
            PassWord = "";
            Role = AccessRole.User;
            FullName = "";
        }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public AccessRole Role { get; set; }
        public String GetFullName()
        {
            return FullName;
        }

    }
}
