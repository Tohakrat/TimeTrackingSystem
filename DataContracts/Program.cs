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
        public User(string username,string password,AccessRole role)
        {
            UserName = username;
            PassWord = password;
            Role = role;
        }
        private string UserName { get; set; }
        private string PassWord { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        private AccessRole Role { get; set; }
        
    }
    public class Project:BaseEntity
    {
        public Project(string name,DateTime expirationDate,int maxHours)
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
    public class TimeTrackEntry : BaseEntity
    {
        public TimeTrackEntry(int userId,int projectId, int value,DateTime date)
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
    public enum AccessRole
    {
        User,
        Admin,
        ProjectLeader
    }
     


}
