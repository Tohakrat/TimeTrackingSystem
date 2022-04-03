using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using DataContracts;
using System.Xml.Serialization;


namespace Repositories.Xml
{
    public class XmlObjectRepository<T> : IRepository<T> where T:BaseEntity
    {
        protected List<T> ObjectList { get; set; } = new();        
        private String FileName { get; set; }
        public XmlObjectRepository(String fileName)
        {
            FileName = fileName;            
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
            
            System.IO.File.WriteAllText(FileName, string.Empty);            
            using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, this.ObjectList);                
            }
        }
        public void DeSerialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));

            try
            {
                using (FileStream Fs = new FileStream(FileName, FileMode.Open))
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

}
