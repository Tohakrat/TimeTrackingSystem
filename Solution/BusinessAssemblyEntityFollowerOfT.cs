using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Xml;
using DataContracts;


namespace Solution
{
    internal class BusinessAssemblyEntityFollowerOfT<T> where T: BaseEntity//This class is needed for the Business assambly controls Repositories.Xml.
    {
        XmlObjectRepository<T> Repository;
        internal BusinessAssemblyEntityFollowerOfT(XmlObjectRepository<T> repository)
        {
            Repository = repository;
            Repository.DeSerialize();
        }
        internal void OnObjectAdded(Object sender, ObjectEventArgs<T> UsEventArs)
        {
            Repository.Insert(UsEventArs.Obj);
            Repository.Serialize();
        }
        internal void OnObjectDeleted(Object sender, ObjectEventArgs<T> UsEventArs)
        {
            Repository.Delete(UsEventArs.Obj);
            Repository.Serialize();
        }
        internal void ConnectAdding(EventHandler<ObjectEventArgs<T>> HandlerToFollow)
        {
            HandlerToFollow += OnObjectAdded;
        }
        internal void ConnectDeleting(EventHandler<ObjectEventArgs<T>> HandlerToFollow)
        {
            HandlerToFollow += OnObjectDeleted;
        }
    }
}
