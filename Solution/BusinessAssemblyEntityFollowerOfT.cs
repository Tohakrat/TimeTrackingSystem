using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Xml;
using DataContracts;


namespace Solution
{
    internal class BusinessAssemblyEntityFollowerOfT<T> where T: BaseEntity//
    {
        XmlObjectRepository<T> Repository;
        internal BusinessAssemblyEntityFollowerOfT(XmlObjectRepository<T> repository)
        {
            Repository = repository;
        }
        internal void OnObjectAdded(Object sender, ObjectEventArgs<T> UsEventArs)
        {
            Repository.Insert(UsEventArs.Obj);
        }
        internal void OnObjectDeleted(Object sender, ObjectEventArgs<T> UsEventArs)
        {
            Repository.Delete(UsEventArs.Obj);
        }
        internal void ConnectAdding(EventHandler<ObjectEventArgs<T>> HandlerToFollow)
        {
            HandlerToFollow += OnObjectAdded;
        }
    }
}
