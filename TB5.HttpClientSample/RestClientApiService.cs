using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class RestClientApiService
{
    public async Task GetMethod(string resourceUri)
    {
        var client = new RestClient(resourceUri);
        var request = new RestRequest();
        var response = await client.ExecuteGetAsync(request);

        if (response.IsSuccessful)
        {
            var jsonStr = response.Content;
            List<BirdModel>? lst = JsonConvert.DeserializeObject<List<BirdModel>>(jsonStr!);
            int count = 0;
            if (lst != null)
            {
                foreach (var item in lst)
                {
                    Console.WriteLine($"{(++count).ToString().PadLeft(2)} : {item.BirdEnglishName}");
                }
            }
        }
        else
        {
            Console.WriteLine("Error: " + response.StatusCode);
        }
    }

    public async Task PostMethod(string resourceUri, BirdRequestModel newBird)
    {
        var client = new RestClient(resourceUri);
        var request = new RestRequest();
        request.AddJsonBody(newBird);

        var response = await client.ExecutePostAsync(request);
        if (response.IsSuccessful)
        {
            Console.WriteLine(response.Content);
        }
        else
        {
            Console.WriteLine("Error: " + response.StatusCode);
        }
    }

    public async Task PutMethod(string resourceUri, BirdRequestModel updateBird)
    {
        var client = new RestClient(resourceUri);
        var request = new RestRequest();
        request.AddJsonBody(updateBird);

        try
        {
            var response = await client.ExecutePutAsync(request);
            if (response.IsSuccessful)
            {
                Console.WriteLine(response.Content);
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
        var client = new RestClient(resourceUri);
        var request = new RestRequest();
        request.Method = Method.Patch;
        request.AddJsonBody(patchBird);

        try
        {
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                Console.WriteLine(response.Content);
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
        var client = new RestClient(resourceUri);
        var request = new RestRequest();
        request.Method = Method.Delete;

        try
        {
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                Console.WriteLine(response.Content);
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
        var client = new RestClient(resourceUri);
        var request = new RestRequest();
        var response = await client.ExecuteGetAsync(request);

        if (response.IsSuccessful)
        {
            Console.WriteLine($"Response: {response.Content}");
        }
        else
        {
            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }
            throw new Exception($"Request failed with status code {response.StatusCode}");
        }
    }
}
