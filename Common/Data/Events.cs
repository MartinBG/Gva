using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data
{
    public static class Events
    { 
        [ThreadStatic]
        private static IList<IEventHandler> handlers = new List<IEventHandler>();

        public static void Register(IEventHandler handler)
        {
            handlers.Add(handler);
        }

        public static void Raise(IEvent e)
        {
            foreach (var handler in handlers)
            {
                handler.Handle(e);
            }
        }
    }
}
