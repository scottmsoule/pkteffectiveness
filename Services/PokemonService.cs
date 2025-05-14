using System.Text.Json;
using PkTEffectiveness.Models;

namespace PkTEffectiveness.Services;

public class PokemonService : IPokemonService
{
    private readonly HttpClient _http;

    public PokemonService(HttpClient http)
    {
        _http = http;
    }

    public async Task<string> GetEffectivenessAsync(string pokemonName)
    {
        var response = await _http.GetAsync($"https://pokeapi.co/api/v2/pokemon/{pokemonName}");
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Pokémon '{pokemonName}' not found.");

        var content = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var pokemonData = JsonSerializer.Deserialize<PokemonResponse>(content, options);

        var allRelations = new List<TypeRelations>();


        foreach (var typeSlot in pokemonData!.Types)
        {
            var typeName = typeSlot.Type.Name;
            var typeResponse = await _http.GetStringAsync($"https://pokeapi.co/api/v2/type/{typeName}");

            var typeData = JsonSerializer.Deserialize<TypeResponse>(typeResponse, options);

            if (typeData == null)
            {
                Console.WriteLine($"Deserialization failed for type: {typeName}");
                continue;
            }

            allRelations.Add(typeData.DamageRelations);
        }


        var strengths = new HashSet<string>();
        var weaknesses = new HashSet<string>();

        foreach (var relation in allRelations)
        {
            // Strength criteria
            relation.DoubleDamageTo.ForEach(t => strengths.Add(t.Name));
            relation.NoDamageFrom.ForEach(t => strengths.Add(t.Name));
            relation.HalfDamageFrom.ForEach(t => strengths.Add(t.Name));

            // Weakness criteria
            relation.NoDamageTo.ForEach(t => weaknesses.Add(t.Name));
            relation.HalfDamageTo.ForEach(t => weaknesses.Add(t.Name));
            relation.DoubleDamageFrom.ForEach(t => weaknesses.Add(t.Name));
        }

        return FormatEffectiveness(pokemonName, strengths, weaknesses);
    }

    private string FormatEffectiveness(string name, HashSet<string> strengths, HashSet<string> weaknesses)
    {
        return $"""
        Effectiveness for: {name.ToUpperInvariant()}
        ------------------------------------------
        Strengths:   {string.Join(", ", strengths.OrderBy(s => s))}
        Weaknesses:  {string.Join(", ", weaknesses.OrderBy(w => w))}
        """;
    }
}
