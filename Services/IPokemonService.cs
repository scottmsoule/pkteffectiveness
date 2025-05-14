namespace PkTEffectiveness.Services;

public interface IPokemonService
{
    Task<string> GetEffectivenessAsync(string pokemonName);
}
