﻿using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Common.Extensions
{
    public static class HttpExtensions
    {
        public static void AddNoCacheHeaders(this HttpResponseMessage response)
        {
            response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
            response.Headers.Add("Pragma", "no-cache");
            response.RequestMessage.GetOwinContext().Response.Headers.AddOrUpdate("Expires", new string[] { DateTime.MinValue.ToString("R") });
        }

        private static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key))
            {
                dict[key] = value;
            }
            else
            {
                dict.Add(key, value);
            }
        }
    }
}
