using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MongoDB.Driver;

namespace audible_biblio
{
    public static class newBook
    {
        [FunctionName("newBook")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("getBooks funktion wird ausgefuehrt");

            var client = new MongoClient("mongodb+srv://fatrixmpj:hahalolXD1@cluster0.9ffjf.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");

            var database = client.GetDatabase("audible-biblio");

            var collection = database.GetCollection<audiobook>("audiobooks");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<audiobook>(requestBody);
            data._id = Guid.NewGuid();

            try
            {
                await collection.InsertOneAsync(data);

                return new OkObjectResult(200);
            }
            catch(Exception ex)
            {
                return new ObjectResult("Exception: " + ex);
            }
        }
    }
}
