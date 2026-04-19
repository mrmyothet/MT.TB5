using Newtonsoft.Json;
using System;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

Console.OutputEncoding = Encoding.UTF8;
Console.WriteLine("こんにちは世界");

string baseAddress = "https://fake-brids-apis.vercel.app/";
string birdsEndpoint = baseAddress + "/api/v1/birds";

BirdRequestModel newBird = new BirdRequestModel {
    BirdEnglishName = "Test Bird",
    BirdMyanmarName = "စမ်းသပ်ငှက်",
    Description = "This is a test bird.",
    ImagePath = "https://example.com/test-bird.jpg"
};


HttpClientApiService apiService = new HttpClientApiService();

//await apiService.GetMethod(birdsEndpoint);

// await apiService.PostMethod(birdsEndpoint, newBird);
// await apiService.PutMethod($"{birdsEndpoint}/1", newBird);
// await apiService.PatchMethod($"{birdsEndpoint}/1", newBird);
// await apiService.DeleteMethod($"{birdsEndpoint}/1");
// await apiService.GetByIdMethod($"{birdsEndpoint}/1");

//RestClientApiService restApiService = new RestClientApiService();

//await restApiService.GetMethod(birdsEndpoint);
//await restApiService.PostMethod(birdsEndpoint, newBird);

RefitClientApiService refitApiService = new RefitClientApiService();

await refitApiService.GetMethod(birdsEndpoint);
await refitApiService.PostMethod(birdsEndpoint, newBird);

Console.ReadLine();



