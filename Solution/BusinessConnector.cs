using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContracts;
using Contracts;
using Repositories.Xml;
using Business;

namespace Solution
{
    internal class BusinessConnector//This class is needed for the Business assambly controls Repositories.Xml.
    {
        private XmlObjectRepository<User> _RepositoryUser;
        private BusinessAssemblyEntityFollowerOfT<User> UserConnector;
        //private IRepository<Project> _RepositoryProject;
        //private IRepository<TimeTrackEntry> _RepositoryTimeTrackEntry;
        public BusinessConnector(XmlObjectRepository<User> repoUser   )
        {
            _RepositoryUser = repoUser;
            UserConnector = new BusinessAssemblyEntityFollowerOfT<User>(_RepositoryUser);
            DataFacade.GetDataFacade().UserServicesObj.UserAdded += UserConnector.OnObjectAdded;
            DataFacade.GetDataFacade().UserServicesObj.UserDeleted += UserConnector.OnObjectDeleted;
        }



        
    }
}
