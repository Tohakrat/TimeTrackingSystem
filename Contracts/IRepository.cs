using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContracts;

namespace Contracts
{
    internal interface IRepository<T> 
    {
        internal IEnumerable<T> GetAll();
        //internal void Insert<T> (T InsertedItem);
    }
    internal class Repository<T> : IRepository<T>
    {
        private List<T> RepoList = new List<T>();
        internal IEnumerable<T> GetAll()
        {
            return RepoList;
        }
        //internal void Insert(T InsertedItem)
        //{
        //    if (InsertedItem!=null)
        //    RepoList.Add(InsertedItem); 
        //}
    }
}
