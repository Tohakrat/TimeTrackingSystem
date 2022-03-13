using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContracts;

namespace DataContracts
{
    public class ObjectEventArgs<T>:EventArgs where T :BaseEntity
    {
        public ObjectEventArgs(T obj)
        {
            Obj = obj;
        }
        public T Obj { get; }
    }
}
