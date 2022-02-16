using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Repositories.Xml;

namespace Solution
{
    internal class Mapper
    {

        IUserRepository GetUserRepository() { return new XmlUserRepository(); }
    }




}
