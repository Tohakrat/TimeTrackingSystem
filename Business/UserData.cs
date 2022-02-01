using System;
using System.Collections.Generic;
using DataContracts;

namespace Business
{   
    public class UserData
    {
        public UserData(User user)
        {
            UserObj = user; 
        }
        User UserObj;
        List<TimeTrackEntry> SybmittedTime = new();
        event StateChanged IsActiveChanged;
    }
    delegate void StateChanged(int UserId, int ProjectId, string message);
    //event AccountHandler Notify;
}
