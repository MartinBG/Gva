using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gva.Web.Jobs
{
    public interface IJob : IDisposable
    {
        void Start();
    }
}