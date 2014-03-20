using System;
using System.Collections.Generic;

namespace Common.Data
{
    public static class Events
    {
        [ThreadStatic]
        private static IList<IEventHandler> handlers;

        private static IList<IEventHandler> Handlers
        {
            get
            {
                // Initializing the static field through a property.

                // From MSDN:
                // Do not specify initial values for fields marked with ThreadStaticAttribute,
                // because such initialization occurs only once, when the class constructor executes
                // and therefore affects only one thread.

                if (handlers == null)
                {
                    handlers = new List<IEventHandler>();
                }

                return handlers;
            }
        }

        public static void Register(IEventHandler handler)
        {
            Handlers.Add(handler);
        }

        public static void Deregister(IEventHandler handler)
        {
            Handlers.Remove(handler);
        }

        public static void Raise(IEvent e)
        {
            foreach (var handler in Handlers)
            {
                handler.Handle(e);
            }
        }
    }
}
