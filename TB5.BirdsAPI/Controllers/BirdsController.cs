using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;
using TB5.BirdsAPI.Models;
using static System.Net.Mime.MediaTypeNames;

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

            return NotFound("No birds found.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ReadAsync(int id)
        {
            var url = $"{ENDPOINT}/{id}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                BirdModel? item = JsonConvert.DeserializeObject<BirdModel>(jsonStr);

                return Ok(item);
            }

            return NotFound("No bird found.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(BirdRequestModel newBird)
        {
            HttpClient client = new HttpClient();

            var createRequestJson = JsonConvert.SerializeObject(newBird);
            StringContent content = new StringContent(createRequestJson, Encoding.UTF8, Application.Json);

            var response = await client.PostAsync(ENDPOINT, content);
            if (response.IsSuccessStatusCode)
            {
                string responseStr = await response.Content.ReadAsStringAsync();
                return Ok(responseStr);
            }

            return BadRequest("Error: " + response.StatusCode);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, BirdRequestModel updateBird)
        {
            HttpClient client = new HttpClient();
            var url = $"{ENDPOINT}/{id}";

            var updateRequestJson = JsonConvert.SerializeObject(updateBird);
            StringContent content = new StringContent(updateRequestJson, Encoding.UTF8, Application.Json);

            try
            {
                var response = await client.PutAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    string responseStr = await response.Content.ReadAsStringAsync();
                    return Ok(responseStr);
                }

                return BadRequest("Error: " + response.StatusCode);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAsync(int id, BirdPatchModel patchBird)
        {
            HttpClient client = new HttpClient();
            var url = $"{ENDPOINT}/{id}";

            var patchRequestJson = JsonConvert.SerializeObject(patchBird);
            StringContent content = new StringContent(patchRequestJson, Encoding.UTF8, Application.Json);

            try
            {
                var response = await client.PatchAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    string responseStr = await response.Content.ReadAsStringAsync();
                    return Ok(responseStr);
                }

                return BadRequest("Error: " + response.StatusCode);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            HttpClient client = new HttpClient();
            var url = $"{ENDPOINT}/{id}";

            try
            {
                var response = await client.DeleteAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string responseStr = await response.Content.ReadAsStringAsync();
                    return Ok(responseStr);
                }

                return BadRequest("Error: " + response.StatusCode);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
