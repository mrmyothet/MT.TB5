using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

public class RefitClientApiService
{
    private IBirdApi GetApiClient(string resourceUri)
    {
        var uri = new Uri(resourceUri);
        var baseUrl = $"{uri.Scheme}://{uri.Authority}";
        return RestService.For<IBirdApi>(baseUrl);
    }

    private string GetIdFromUri(string uri)
    {
        var uriObj = new Uri(uri);
        var path = uriObj.AbsolutePath.TrimEnd('/');
        var lastSlashIdx = path.LastIndexOf('/');
        if (lastSlashIdx != -1)
             return path.Substring(lastSlashIdx + 1);
        return "";
    }

    public async Task GetMethod(string resourceUri)
    {
        var api = GetApiClient(resourceUri);
        var response = await api.GetBirds();

        if (response.IsSuccessStatusCode)
        {
            var lst = response.Content;
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
        var api = GetApiClient(resourceUri);
        try
        {
            var response = await api.CreateBird(newBird);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
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

    public async Task PutMethod(string resourceUri, BirdRequestModel updateBird)
    {
        var api = GetApiClient(resourceUri);
        var id = GetIdFromUri(resourceUri);
        try
        {
            var response = await api.UpdateBird(id, updateBird);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
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
        var api = GetApiClient(resourceUri);
        var id = GetIdFromUri(resourceUri);
        try
        {
            var response = await api.PatchBird(id, patchBird);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
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
        var api = GetApiClient(resourceUri);
        var id = GetIdFromUri(resourceUri);
        try
        {
            var response = await api.DeleteBird(id);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
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
        var api = GetApiClient(resourceUri);
        var id = GetIdFromUri(resourceUri);
        try
        {
            var response = await api.GetBirdById(id);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response: {content}");
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
