using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using CustomersServices.Model;
using CustomersServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace CustomersFunction
{
    public static class CustomersFunction
    {
        [FunctionName("CustomersFunction")]
        [OpenApiOperation(operationId: "Customers", tags: new[] { "get" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/json", bodyType: typeof(List<Customer>), Description = "The list of customers")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            ICustomerService service = new CustomerService();
            var customers = service.GetCustomers();

            return new OkObjectResult(customers);
        }
    }
}