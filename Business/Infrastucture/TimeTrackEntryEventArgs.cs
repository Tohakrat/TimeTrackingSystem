using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContracts;

namespace Business.Infrastucture
{
    public class TimeTrackEntryEventArgs
    {
        public TimeTrackEntryEventArgs(TimeTrackEntry T)
        {
            TimeTrackEntry = T;
        }
        public TimeTrackEntry TimeTrackEntry { get; }

    }
}
