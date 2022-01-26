using System;

namespace DataContracts
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public string Comment { get; set; }

    }
    public class User:BaseEntity
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public enum AccessRole
        {
            User,
            Admin,
            ProjectLeader            
        }
    }
    public class Project:BaseEntity
    {
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int MaxHours { get; set; }
        public int LeaderUserId { get; set; }
    }
    public class TimeTrackEntry : BaseEntity
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }        
        public int Value { get; set; }
        public DateTime Date { get; set; }
    }




}
