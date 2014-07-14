using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace Common.Owin
{
    public class SessionMiddleware
    {
        public const string OwinEnvironmentKey = "common.Session";
        public const string CookieName = "sessionCookie";

        private readonly AppFunc next;

        public SessionMiddleware(AppFunc next)
        {
            this.next = next;
        }

        private IEnumerable<Tuple<string, string>> ParseCookie(string cookie)
        {
            var split = cookie.Split(';');
            foreach (var item in split)
            {
                var split2 = item.Split('=');
                if (split2.Length != 2) continue;
                yield return new Tuple<string, string>(split2[0].Trim(), split2[1].Trim());
            }
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            var requestHeaders = environment["owin.RequestHeaders"] as IDictionary<string, string[]>;

            string sessionKey = null;

            string[] cookies;
            if (requestHeaders.TryGetValue("Cookie", out cookies))
            {
                var cookieValue = cookies
                    .SelectMany(x => ParseCookie(x))
                    .FirstOrDefault(x => x.Item1 == CookieName);
                if (cookieValue != null)
                {
                    sessionKey = cookieValue.Item2;
                }
            }
            
            if (string.IsNullOrEmpty(sessionKey))
            {
                sessionKey = Guid.NewGuid().ToString();

                string sessionCookieValue = string.Format(
                    "{0}={1}",
                    Uri.EscapeDataString(CookieName),
                    Uri.EscapeDataString(sessionKey));

                var responseHeaders = environment["owin.ResponseHeaders"] as IDictionary<string, string[]>;

                string[] setCookieContainer;
                if (!responseHeaders.TryGetValue("Set-Cookie", out setCookieContainer))
                {
                    setCookieContainer = new string[0];
                }

                var dest = new string[setCookieContainer.Length + 1];
                Array.Copy(setCookieContainer, dest, setCookieContainer.Length);
                dest[dest.Length - 1] = sessionCookieValue;

                responseHeaders["Set-Cookie"] = dest;
            }

            environment[OwinEnvironmentKey] = sessionKey;

            await next(environment);
        }
    }
}
