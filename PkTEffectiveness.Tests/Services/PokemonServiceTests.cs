using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PkTEffectiveness.Services;
using Xunit;
using PkTEffectiveness.Tests.Helpers;

namespace PkTEffectiveness.Tests.Services;

public class PokemonServiceTests
{
    [Fact]
    public async Task GetEffectivenessAsync_ReturnsFormattedString_ForPikachu()
    {
        // Arrange
        var pikachuJson = """
        {
          "types": [
            {
              "slot": 1,
              "type": {
                "name": "electric",
                "url": "https://pokeapi.co/api/v2/type/13/"
              }
            }
          ]
        }
        """;

        var typeJson = """
        {
          "damage_relations": {
            "double_damage_to": [
              { "name": "water", "url": "" },
              { "name": "flying", "url": "" }
            ],
            "no_damage_from": [],
            "half_damage_from": [],
            "no_damage_to": [],
            "half_damage_to": [],
            "double_damage_from": [
              { "name": "ground", "url": "" }
            ]
          }
        }
        """;

        var handler = new FakeHttpMessageHandler((req, _) =>
        {
            if (req.RequestUri!.AbsoluteUri.Contains("/pokemon/pikachu"))
            {
                return Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(pikachuJson, Encoding.UTF8, "application/json")
                });
            }
            else if (req.RequestUri!.AbsoluteUri.Contains("/type/electric"))
            {
                return Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(typeJson, Encoding.UTF8, "application/json")
                });
            }

            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));
        });

        var httpClient = new HttpClient(handler);
        var service = new PokemonService(httpClient);

        // Act
        var result = await service.GetEffectivenessAsync("pikachu");

        // Assert
        Assert.Contains("Effectiveness for: PIKACHU", result);
        Assert.Contains("Strengths:   flying, water", result);
        Assert.Contains("Weaknesses:  ground", result);
    }
}
