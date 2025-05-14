using System.Text.Json.Serialization;

public class PokemonResponse
{
    [JsonPropertyName("types")]
    public List<TypeSlot> Types { get; set; } = new();

}

public class TypeSlot
{
    [JsonPropertyName("slot")]
    public int Slot { get; set; }

    [JsonPropertyName("type")]
    public NamedApiResource Type { get; set; } = new();
}

public class NamedApiResource
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}
