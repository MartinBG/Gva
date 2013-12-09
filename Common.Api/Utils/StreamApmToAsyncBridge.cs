using System;
using System.IO;
using System.Threading.Tasks;

namespace Common.Api.Utils
{
    public class StreamApmToAsyncBridge : Stream
    {
        private Stream stream;

        public StreamApmToAsyncBridge(Stream stream)
        {
            this.stream = stream;
        }

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return this.stream.ReadAsync(buffer, offset, count).ToApm(callback, state);
        }

        public override int EndRead(IAsyncResult asyncResult)
        {
            try
            {
                return ((Task<int>)asyncResult).Result;
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException;
            }
        }

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return this.stream.WriteAsync(buffer, offset, count).ToApm(callback, state);
        }

        public override void EndWrite(IAsyncResult asyncResult)
        {
            Task task = ((Task)asyncResult);
            if (task.Exception != null)
            {
                throw task.Exception.InnerException;
            }
        }

        public override bool CanRead
        {
            get { return this.stream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return this.stream.CanSeek; }
        }

        public override bool CanTimeout
        {
            get { return this.stream.CanTimeout; }
        }

        public override bool CanWrite
        {
            get { return this.stream.CanWrite; }
        }

        public override void Close()
        {
            this.stream.Close();
        }

        public override Task CopyToAsync(Stream destination, int bufferSize, System.Threading.CancellationToken cancellationToken)
        {
            return this.stream.CopyToAsync(destination, bufferSize, cancellationToken);
        }

        public override System.Runtime.Remoting.ObjRef CreateObjRef(Type requestedType)
        {
            return this.stream.CreateObjRef(requestedType);
        }

        public override bool Equals(object obj)
        {
            return this.stream.Equals(obj);
        }

        public override void Flush()
        {
            this.stream.Flush();
        }

        public override Task FlushAsync(System.Threading.CancellationToken cancellationToken)
        {
            return this.stream.FlushAsync(cancellationToken);
        }

        public override int GetHashCode()
        {
            return this.stream.GetHashCode();
        }

        public override object InitializeLifetimeService()
        {
            return this.stream.InitializeLifetimeService();
        }

        public override long Length
        {
            get { return this.stream.Length; }
        }

        public override long Position
        {
            get
            {
                return this.stream.Position;
            }
            set
            {
                this.stream.Position = value;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return this.stream.Read(buffer, offset, count);
        }

        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, System.Threading.CancellationToken cancellationToken)
        {
            return this.stream.ReadAsync(buffer, offset, count, cancellationToken);
        }

        public override int ReadByte()
        {
            return this.stream.ReadByte();
        }

        public override int ReadTimeout
        {
            get
            {
                return this.stream.ReadTimeout;
            }
            set
            {
                this.stream.ReadTimeout = value;
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return this.stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            this.stream.SetLength(value);
        }

        public override string ToString()
        {
            return this.stream.ToString();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            this.stream.Write(buffer, offset, count);
        }

        public override Task WriteAsync(byte[] buffer, int offset, int count, System.Threading.CancellationToken cancellationToken)
        {
            return this.stream.WriteAsync(buffer, offset, count, cancellationToken);
        }

        public override void WriteByte(byte value)
        {
            this.stream.WriteByte(value);
        }

        public override int WriteTimeout
        {
            get
            {
                return this.stream.WriteTimeout;
            }
            set
            {
                this.stream.WriteTimeout = value;
            }
        }
    }
}
