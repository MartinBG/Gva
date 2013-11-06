using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Common.Http
{
    public interface IMvcConfig
    {
        void RegisterRoutes(RouteCollection routes);
    }
}
