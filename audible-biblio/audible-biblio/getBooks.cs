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
    public static class getBooks
    {
        [FunctionName("getBooks")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("getBooks funktion wird ausgefuehrt");

            var client = new MongoClient("mongodb+srv://fatrixmpj:hahalolXD1@cluster0.9ffjf.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");

            var database = client.GetDatabase("audible-biblio");

            var collection = database.GetCollection<audiobook>("audiobooks");

            var documents = await collection.Find(x => true).ToListAsync();

            return new OkObjectResult(documents);
        }
    }
}
