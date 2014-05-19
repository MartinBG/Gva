using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Hosting;

namespace Common.Jobs
{
    public class JobHost : IRegisteredObject, IDisposable
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly object jobLock = new object();

        private IJob job;
        private Timer timer;

        public JobHost(IJob job)
        {
            this.job = job;
            this.timer = new Timer(this.DoAction);

            HostingEnvironment.RegisterObject(this);
        }

        // reads & writes of bool are atomic, so no lock is required
        public bool IsShuttingDown { get; private set; }

        public void Start()
        {
            logger.Info(string.Format("{0} Initializing.", this.job.Name));

            this.timer.Change(TimeSpan.FromSeconds(0), this.job.Period);
        }

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

        public void DoAction(object sender)
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
                    logger.Info(string.Format("{0} Started.", this.job.Name));

                    this.job.Action();

                    logger.Info(string.Format("{0} Finished.", this.job.Name));
                }
                finally
                {
                    Monitor.Exit(this.jobLock);
                }
            }
        }

        public void Dispose()
        {
            this.timer.Dispose();

            logger.Info(string.Format("{0} Disposed.", this.job.Name));
        }
    }
}