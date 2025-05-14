using Microsoft.Extensions.DependencyInjection;
using PkTEffectiveness.Services;

var services = new ServiceCollection()
    .AddHttpClient()
    .AddTransient<IPokemonService, PokemonService>()
    .BuildServiceProvider();

var pokemonService = services.GetRequiredService<IPokemonService>();

Console.WriteLine("Enter a Pokémon name:");
var name = Console.ReadLine()?.Trim().ToLower();

if (string.IsNullOrWhiteSpace(name))
{
    Console.WriteLine("No Pokémon name provided.");
    return;
}

try
{
    var result = await pokemonService.GetEffectivenessAsync(name);
    Console.WriteLine(result);
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
