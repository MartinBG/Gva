using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gva.MigrationTool.Blobs
{
    public class RateLimiter
    {
        private class IncrementWaiter
        {
            public long Increment { get; set; }
            public ManualResetEvent WaitHandle { get; set; }
        }

        private object syncRoot = new object();
        private long currentSize;
        private long maximumSize;
        private CancellationToken cancellationToken;
        private Queue<IncrementWaiter> incrementQueue = new Queue<IncrementWaiter>();

        public RateLimiter(long initialSize, long maximumSize, CancellationToken cancellationToken)
        {
            this.currentSize = initialSize;
            this.maximumSize = maximumSize;
            this.cancellationToken = cancellationToken;
        }

        public long CurrentSize
        {
            get
            {
                return this.currentSize;
            }
        }

        public void Increment(long increment)
        {
            if (increment > this.maximumSize)
            {
                throw new ArgumentException("increment is bigger than maximum size");
            }

            ManualResetEvent waitHandle = null;
            lock (this.syncRoot)
            {
                if (this.currentSize + increment > this.maximumSize)
                {
                    waitHandle = new ManualResetEvent(false);
                    this.incrementQueue.Enqueue(new IncrementWaiter { Increment = increment, WaitHandle = waitHandle });
                }
                else
                {
                    this.currentSize += increment;
                }
            }

            if (waitHandle != null)
            {
                int eventThatSignaledIndex = WaitHandle.WaitAny(new WaitHandle[] { waitHandle, this.cancellationToken.WaitHandle });
                waitHandle.Dispose();
                if (eventThatSignaledIndex == 1)
                {
                    throw new OperationCanceledException(this.cancellationToken);
                }
            }
        }

        public void Decrement(long decrement)
        {
            lock (this.syncRoot)
            {
                this.currentSize -= decrement;

                while (this.incrementQueue.Count > 0)
                {
                    var nextIncrement = this.incrementQueue.Peek();
                    if (this.currentSize + nextIncrement.Increment < this.maximumSize)
                    {
                        this.incrementQueue.Dequeue();
                        this.currentSize += nextIncrement.Increment;
                        nextIncrement.WaitHandle.Set();
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}
