using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContracts
{
    public class Project : BaseEntity
    {
        public Project(int id,string name, DateTime expirationDate, int maxHours,int leaderUserId)
        {
            Id = id;
            Name = name;
            ExpirationDate = expirationDate;
            MaxHours = maxHours;
            LeaderUserId = leaderUserId;
        }
        internal string Name { get; set; }
        internal DateTime ExpirationDate { get; set; }
        internal int MaxHours { get; set; }
        internal int LeaderUserId { get; set; }
    }
}
