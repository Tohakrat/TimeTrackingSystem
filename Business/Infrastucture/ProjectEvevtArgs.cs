﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContracts;

namespace Infrastructure
{
    public class ProjectEventArgs : EventArgs
    {
        public ProjectEventArgs(Project P)
        {
            ProjectObj = P;
        }
        public Project ProjectObj { get; }
    }
}
