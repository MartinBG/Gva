﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Api.Jobs
{
    public interface IJob
    {
        string Name { get; }
        TimeSpan Period { get; }
        void Action();
    }
}