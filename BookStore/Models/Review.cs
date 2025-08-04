using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace BookStoreManager.Models;

public class Review
{

    [JsonPropertyName("author")]
    public string Author {  get; set; }

    [JsonPropertyName("text")]
    public string Text {  get; set; }
}
