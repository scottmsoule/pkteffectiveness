namespace PkTEffectiveness.Models;

public class PokemonResponse
{
    public List<TypeSlot> Types { get; set; } = new();
}

public class TypeSlot
{
    public int Slot { get; set; }
    public NamedApiResource Type { get; set; } = new();
}

public class NamedApiResource
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}
