﻿using System.Net.Http;
using System.Web.Http.Filters;
using Common.Extensions;

namespace Common.Filters
{
    public class NoCacheActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Request.Method == HttpMethod.Get)
            {
                actionExecutedContext.Response.AddNoCacheHeaders();
            }
        }
    }
}

