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
        internal string UserName { get; set; }
        internal string PassWord { get; set; }
        internal string FullName { get; set; }
        internal bool IsActive { get; set; }
        internal AccessRole Role { get; set; }

    }
}
