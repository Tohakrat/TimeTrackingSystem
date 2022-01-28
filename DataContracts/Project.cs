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
        private string Name { get; set; }
        private DateTime ExpirationDate { get; set; }
        public int MaxHours { get; set; }
        public int LeaderUserId { get; set; }
    }
}
