using System.Text.Json.Serialization;

namespace Tamagotchi
{
    public class PokemonModel
    {

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("weight")]

        public int Weight { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("stats")]
        public List<StatsEntry>? Stats { get; set; }
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