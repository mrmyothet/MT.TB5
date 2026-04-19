using Refit;

public interface IBirdApi
{
    [Get("/api/v1/birds")]
    Task<ApiResponse<List<BirdModel>>> GetBirds();

    [Get("/api/v1/birds/{id}")]
    Task<HttpResponseMessage> GetBirdById(string id);

    [Post("/api/v1/birds")]
    Task<HttpResponseMessage> CreateBird([Body] BirdRequestModel bird);

    [Put("/api/v1/birds/{id}")]
    Task<HttpResponseMessage> UpdateBird(string id, [Body] BirdRequestModel bird);

    [Patch("/api/v1/birds/{id}")]
    Task<HttpResponseMessage> PatchBird(string id, [Body] BirdRequestModel bird);

    [Delete("/api/v1/birds/{id}")]
    Task<HttpResponseMessage> DeleteBird(string id);
}
