﻿using System;

using Azure.WebJobs.Extensions.HttpApi;

using KeyVault.Acmebot.Internal;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace KeyVault.Acmebot.Functions
{
    public class StaticPage : HttpFunctionBase
    {
        public StaticPage(IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
        }

        [FunctionName(nameof(StaticPage) + "_" + nameof(Dashboard))]
        public IActionResult Dashboard(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "static-page/dashboard")] HttpRequest req,
            ILogger log)
        {
            if (!IsEasyAuthEnabled || !User.IsAppAuthorized())
            {
                return Forbid();
            }

            return File("static/dashboard.html");
        }

        [FunctionName(nameof(StaticPage) + "_" + nameof(AddCertificate))]
        public IActionResult AddCertificate(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "static-page/add-certificate")] HttpRequest req,
            ILogger log)
        {
            if (!IsEasyAuthEnabled || !User.IsAppAuthorized())
            {
                return Forbid();
            }

            return File("static/add-certificate.html");
        }

        [FunctionName(nameof(StaticPage) + "_" + nameof(BulkCertificate))]
        public IActionResult BulkCertificate(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "static-page/bulk-certificate")] HttpRequest req,
            ILogger log)
        {
            if (!IsEasyAuthEnabled || !User.Identity.IsAuthenticated)
            {
                return Forbid();
            }

            return File("static/bulk-certificate.html");
        }

        private static bool IsEasyAuthEnabled => bool.TryParse(Environment.GetEnvironmentVariable("WEBSITE_AUTH_ENABLED"), out var result) && result;
    }
}
