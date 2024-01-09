using System.Text.Json.Serialization;

namespace Tamagotchi
{
    public class PokemonModel
    {

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("weight")]

        public int Weight { get; set; }
        [JsonPropertyName("stats")]
        public List<StatsEntry>? Stats { get; set; }

        [JsonPropertyName("abilities")]
        public List<AbilityEntry>? Abilities { get; set; }
    }

    public class AbilityEntry
    {
        [JsonPropertyName("ability")]
        public Ability? Ability { get; set; }
    }

    public class Ability
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    public class StatsEntry
    {
        [JsonPropertyName("base_stat")]
        public int BaseStat { get; set; }

        [JsonPropertyName("stat")]
        public Stat? Stat { get; set; }
    }

    public class Stat
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}