using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Owin
{
    public class RerouteMiddleware
    {
        private readonly Func<IDictionary<string, object>, Task> next;
        private string fromPath;
        private string toPath;

        public RerouteMiddleware(Func<IDictionary<string, object>, Task> next, string fromPath, string toPath)
        {
            this.next = next;
            this.fromPath = fromPath;
            this.toPath = toPath;
        }

        public Task Invoke(IDictionary<string, object> environment)
        {
            if ((string)environment["owin.RequestPath"] == fromPath)
            {
                environment["owin.RequestPath"] = toPath;
            }
            return next(environment);
        }
    }
}