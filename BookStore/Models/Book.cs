using System.Text.Json.Serialization;

namespace BookStoreManager.Models;

public class Book
{
    [JsonPropertyName("index")]
    public int Index {  get; set; }

    [JsonPropertyName("isbn")]
    public string Isbn { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("author")]
    public string Author { get; set; }

    [JsonPropertyName("publisher")]
    public string Publisher { get; set; }

    [JsonPropertyName("likes")]
    public int Likes { get; set; }

    [JsonPropertyName("reviews")]
    public List<Review> Reviews { get; set; } = new();
}
