using Newtonsoft.Json;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

public class HttpClientApiService
{
    public async Task GetMethod(string resourceUri)
    {
        HttpClient httpClient = new HttpClient();
        //httpClient.BaseAddress = new Uri(resourceUri);
        HttpResponseMessage response = await httpClient.GetAsync(resourceUri);

        //response.EnsureSuccessStatusCode();

        if (response.IsSuccessStatusCode)
        {
            var jsonStr = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(jsonStr);

            List<BirdModel>? lst = JsonConvert.DeserializeObject<List<BirdModel>>(jsonStr);
            int count = 0;
            foreach (var item in lst)
            {
                //Console.WriteLine(item.Id);
                //Console.WriteLine(item.BirdEnglishName);
                //Console.WriteLine(item.BirdMyanmarName);
                //Console.WriteLine(item.Description);
                //Console.WriteLine(item.ImagePath);
                //Console.WriteLine();

                Console.WriteLine($"{(++count).ToString().PadLeft(2)} : {item.BirdEnglishName}");
            }
        }
        else
        {
            Console.WriteLine("Error: " + response.StatusCode);
        }
    }

    public async Task PostMethod(string resourceUri, BirdRequestModel newBird)
    {
        HttpClient httpClient = new HttpClient();

        var createRequestJson = JsonConvert.SerializeObject(newBird);
        StringContent content = new StringContent(createRequestJson, Encoding.UTF8, Application.Json);

        var response = await httpClient.PostAsync(resourceUri, content);
        if (response.IsSuccessStatusCode)
        {
            string responseStr = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseStr);
        }
        else
        {
            Console.WriteLine("Error: " + response.StatusCode);
        }
    }

    public async Task PutMethod(string resourceUri, BirdRequestModel updateBird)
    {
        HttpClient httpClient = new HttpClient();

        var updateRequestJson = JsonConvert.SerializeObject(updateBird);
        StringContent content = new StringContent(updateRequestJson, Encoding.UTF8, Application.Json);

        try
        {
            var response = await httpClient.PutAsync(resourceUri, content);
            if (response.IsSuccessStatusCode)
            {
                string responseStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseStr);
            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task PatchMethod(string resourceUri, BirdRequestModel patchBird)
    {
        HttpClient httpClient = new HttpClient();

        var patchRequestJson = JsonConvert.SerializeObject(patchBird);
        StringContent content = new StringContent(patchRequestJson, Encoding.UTF8, Application.Json);

        try
        {
            var response = await httpClient.PatchAsync(resourceUri, content);
            if (response.IsSuccessStatusCode)
            {
                string responseStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseStr);
            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task DeleteMethod(string resourceUri)
    {
        HttpClient httpClient = new HttpClient();

        try
        {
            var response = await httpClient.DeleteAsync(resourceUri);
            if (response.IsSuccessStatusCode)
            {
                string responseStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseStr);
            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task GetByIdMethod(string resourceUri)
    {
        HttpClient httpClient = new HttpClient();
        using HttpResponseMessage response = await httpClient.GetAsync(resourceUri);
        response.EnsureSuccessStatusCode();

        string content = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Response: {content}");
    }
}