using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using DataContracts;
using System.Xml.Serialization;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace Repositories.Xml
{
    public class XmlObjectRepository<T> : IRepository<T> where T:BaseEntity
    {
        protected List<T> ObjectList { get; set; } = new();
        public XmlObjectRepository()
        {
            IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
            Settings settings = config.GetRequiredSection("Settings").Get<Settings>();
            // Get values from the config given their key and their target type.
            //AppSettings settings = config.GetRequiredSection("Settings").Get<Settings>();

            // Write the values to the console.
            Console.WriteLine($"UserListFile = {settings.UserListFile}");
        }
        public IEnumerable<T> GetAll()
        {
            return ObjectList;
        }
        public void Insert (T TObj)
        {
            ObjectList.Add(TObj);
            ObjectInserted?.Invoke(this, " Xml object inserted: " + TObj.ToString());
        }
        public void Delete(T TObj)
        {
            T ObjectToDelete = ObjectList.Single(x => x.Id.Equals( TObj.Id)); 
            if (ObjectList.Remove(ObjectToDelete))
            {                    
                ObjectDeleted?.Invoke(this, " Xml object Deleted: " + TObj.ToString());
            }            
        }
        public void Serialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));

            System.IO.File.WriteAllText("UserList.xml", string.Empty);
            using (FileStream fs = new FileStream("UserList.xml", FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, this.ObjectList);
                
            }
        }
        public void DeSerialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));

            try
            {
                using (FileStream Fs = new FileStream("UserList.xml", FileMode.Open))
                {
                    try
                    {
                        ObjectList = (List<T>)xmlSerializer.Deserialize(Fs);
                    }
                    catch (InvalidOperationException ex)
                    {
                        ObjectList = new List<T>();
                    }
                    
                    
                }
            }
            catch (FileNotFoundException ex)
            {
                ObjectList = new List<T>();                
            }
        }
        public event EventHandler<string> ObjectInserted;
        public event EventHandler<string> ObjectDeleted;
    }

    public class Settings
    {
        public string UserListFile { get; set; }

    }

}
