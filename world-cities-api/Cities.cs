using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace world_cities_api
{
    public static class Cities
    {
        [FunctionName("world")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "country", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "The **Country** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"World api requested !");

            string country = req.Query["country"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            country ??= data?.country;

            IEnumerable<World> world = Array.Empty<World>();

            // this could use azure storage
            var folder = "Data";
            var filename = "worldcities.csv"; 

            var binDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var rootDirectory = Path.GetFullPath(Path.Combine(binDirectory, ".."));

            var path = Path.Combine(rootDirectory,folder,filename);
            
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower(),
            };


            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, config))
            {
                world = csv.GetRecords<World>().ToList();
            }


            if (string.IsNullOrEmpty(country))
            {

                var distinctCountries = world.GroupBy(x => x.Country).Select(y => y.First()).ToList().Select(z => z.Country).OrderBy(s => s);
                return new OkObjectResult(distinctCountries);
            }
            else
            {
                var cities = world.Where(x => x.Country.ToLower().Trim() == country.ToLower().Trim()).ToList();

                if(cities.Count == 0)
                {
                    string clientIP = GetIpFromRequestHeaders(req);
                    log.LogInformation($"Possible evil user detected at : {clientIP}");
                    return new OkObjectResult("Bad country name format!");
                }
                else
                    return new OkObjectResult(cities);
            }
            
        }

        private static string GetIpFromRequestHeaders(HttpRequest request)
        {
            return (request.Headers["X-Forwarded-For"].FirstOrDefault() ?? "").Split(new char[] { ':' }).FirstOrDefault();
        }

    }
}

