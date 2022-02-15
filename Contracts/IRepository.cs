using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContracts;

namespace Contracts
{
    internal interface IRepository<T> where T:BaseEntity
    {
        internal IEnumerable<T> GetAll();
        internal void Insert<T> ();
    }
}
