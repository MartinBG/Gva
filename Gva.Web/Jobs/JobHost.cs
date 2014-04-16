using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Hosting;

namespace Gva.Web.Jobs
{
    public class JobHost : IRegisteredObject
    {
        private readonly object jobLock = new object();

        public JobHost()
        {
            HostingEnvironment.RegisterObject(this);
        }

        // reads & writes of bool are atomic, so no lock is required
        public bool IsShuttingDown { get; private set; }

        public void Stop(bool immediate)
        {
            this.IsShuttingDown = true;

            if (immediate)
            {
                // wait for the lock to be sure the task has finished
                lock (this.jobLock)
                {
                    HostingEnvironment.UnregisterObject(this);
                }
            }
        }

        public void DoAction(Action action)
        {
            if (this.IsShuttingDown)
            {
                return;
            }

            // DoAction returns immediately if the previous action has not finished
            if (Monitor.TryEnter(this.jobLock))
            {
                try
                {
                    action();
                }
                finally
                {
                    Monitor.Exit(this.jobLock);
                }
            }
        }
    }
}