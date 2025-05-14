namespace PkTEffectiveness.Models;

using System.Text.Json.Serialization;

public class TypeResponse
{
    [JsonPropertyName("damage_relations")]
    public TypeRelations DamageRelations { get; set; } = new();
}


public class TypeRelations
{
    [JsonPropertyName("double_damage_to")]
    public List<NamedApiResource> DoubleDamageTo { get; set; } = new();

    [JsonPropertyName("double_damage_from")]
    public List<NamedApiResource> DoubleDamageFrom { get; set; } = new();

    [JsonPropertyName("half_damage_to")]
    public List<NamedApiResource> HalfDamageTo { get; set; } = new();

    [JsonPropertyName("half_damage_from")]
    public List<NamedApiResource> HalfDamageFrom { get; set; } = new();

    [JsonPropertyName("no_damage_to")]
    public List<NamedApiResource> NoDamageTo { get; set; } = new();

    [JsonPropertyName("no_damage_from")]
    public List<NamedApiResource> NoDamageFrom { get; set; } = new();
}

