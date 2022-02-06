using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContracts
{
    public class Project : BaseEntity
    {
        public Project(string name, DateTime expirationDate, int maxHours)
        {
            Name = name;
            ExpirationDate = expirationDate;
            MaxHours = maxHours;
        }
        internal string Name { get; set; }
        internal DateTime ExpirationDate { get; set; }
        internal int MaxHours { get; set; }
        internal int LeaderUserId { get; set; }
    }
}
