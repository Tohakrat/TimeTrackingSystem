using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContracts
{
    public class TimeTrackEntry : BaseEntity
    {
        public TimeTrackEntry(int userId, int projectId, int value, DateTime date)
        {
            UserId = userId;
            ProjectId = projectId;
            Value = value;
            Date = date;
        }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public int Value { get; set; }
        public DateTime Date { get; set; }
    }
}
