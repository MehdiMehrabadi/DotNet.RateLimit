﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace DotNet.RateLimiter.Extensions
{
    public static class HttpRequestExtension
    {
        /// <summary>
        /// get current user Ip address based on header name
        /// </summary>
        /// <param name="request"></param>
        /// <param name="headerName"></param>
        /// <returns></returns>
        public static IPAddress GetUserIp(this HttpRequest request, string headerName)
        {
            if (request.Headers[headerName].FirstOrDefault() != null)
            {
                return request.Headers[headerName].FirstOrDefault().GetIpAddresses().FirstOrDefault();
            }

            return request.HttpContext.Connection.RemoteIpAddress;
        }

        /// <summary>
        /// get ip addresses
        /// </summary>
        /// <param name="ips"></param>
        /// <param name="separator">ips separator(default is ',')</param>
        /// <returns></returns>
        public static List<IPAddress> GetIpAddresses(this string ips, string separator = ",")
        {
            var ipAddresses = new List<IPAddress>();
            var ipList = ips.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);

            if (ipList.Length == 0)
                return default;

            foreach (var ip in ipList)
            {
                if (IPAddress.TryParse(ip.Trim(), out var ipAddress))
                    ipAddresses.Add(ipAddress);
            }

            return ipAddresses;
        }

    }
}