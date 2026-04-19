using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using TB5.BirdsAPI.Models;

namespace TB5.BirdsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdsController : ControllerBase
    {
        private const string BASE_ADDRESS = "https://fake-brids-apis.vercel.app/";
        private const string ENDPOINT = BASE_ADDRESS + "/api/v1/birds";

        [HttpGet]
        public async Task<IActionResult> ReadAsync()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(ENDPOINT);

            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(jsonStr);

                List<BirdModel>? lst = JsonConvert.DeserializeObject<List<BirdModel>>(jsonStr);

                return Ok(lst);
            }

            return NotFound("No products found.");
        }
    }
}
