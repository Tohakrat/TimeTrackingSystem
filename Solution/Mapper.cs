using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Repositories.Xml;
using DataContracts;

namespace Solution
{
    public class Mapper
    {
        //XmlUserRepository<T> Repo;
        public IRepository<User> GetUserRepository() { return new XmlObjectRepository<User>(); }
    }




}
