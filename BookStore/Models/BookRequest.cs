using System.Text.Json.Serialization;

namespace BookStoreManager.Models;

public class BookRequest
{
    [JsonPropertyName("seed")]
    public int Seed {  get; set; }

    [JsonPropertyName("page")]
    public int Page {  get; set; }

    [JsonPropertyName("lang")]
    public string Language {  get; set; } = "en";

    [JsonPropertyName("avgLikes")]
    public float AverageLikes { get; set; } = 0;

    [JsonPropertyName("avgReviews")]
    public float AverageReviews { get; set; } = 0;
}
