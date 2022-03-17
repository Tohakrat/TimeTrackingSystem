using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContracts;
using System.Xml.Serialization;

namespace Repositories.Xml
{
    public class XmlUserRepository : XmlObjectRepository<User>
    {
        public void Serialize()
        {                       
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<User>));

            
            using (FileStream fs = new FileStream("UserList.xml", FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, this.ObjectList);

                Console.WriteLine("Object has been serialized");
            }
        }
    }
}
