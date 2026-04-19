using Newtonsoft.Json;
using System;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

Console.OutputEncoding = Encoding.UTF8;
//Console.WriteLine("こんにちは世界"); // Example UTF-8 text

string baseAddress = "https://fake-brids-apis.vercel.app/";
string birdsEndpoint = baseAddress + "/api/v1/birds";

//await GetMethod(birdsEndpoint);

BirdRequestModel newBird = new BirdRequestModel {
    BirdEnglishName = "Test Bird",
    BirdMyanmarName = "စမ်းသပ်ငှက်",
    Description = "This is a test bird.",
    ImagePath = "https://example.com/test-bird.jpg"
};

//await PostMethod(birdsEndpoint, newBird);

await PutMethod($"{birdsEndpoint}/1", newBird);

//await PatchMethod($"{birdsEndpoint}/1", newBird);

//await DeleteMethod($"{birdsEndpoint}/1");

//await GetByIdMethod($"{birdsEndpoint}/20");

Console.ReadLine();

async Task GetMethod(string resourceUri)
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

async Task PostMethod(string resourceUri, BirdRequestModel newBird)
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

async Task PutMethod(string resourceUri, BirdRequestModel updateBird)
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
            Console.WriteLine($"Error: {response.StatusCode}");
            Console.WriteLine($"Requested URI: {resourceUri}");
            string errorContent = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(errorContent))
            {
                Console.WriteLine($"Error details: {errorContent}");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception: {ex.Message}");
    }
}

async Task PatchMethod(string resourceUri, BirdRequestModel patchBird)
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

async Task DeleteMethod(string resourceUri)
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

async Task GetByIdMethod(string resourceUri)
{
    HttpClient httpClient = new HttpClient();
    using HttpResponseMessage response = await httpClient.GetAsync(resourceUri);
    response.EnsureSuccessStatusCode();
    
    string content = await response.Content.ReadAsStringAsync();
    Console.WriteLine($"Response: {content}");
}

public class BirdModel
{
    public int Id { get; set; }
    public string BirdMyanmarName { get; set; }
    public string BirdEnglishName { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
}



public class BirdRequestModel
{
    public string BirdMyanmarName { get; set; }
    public string BirdEnglishName { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
}




