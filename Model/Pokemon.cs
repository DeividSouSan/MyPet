using System.Text.Json.Serialization;

namespace Pet
{
    public class PetModel
    {

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("weight")]

        public int Weight { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("stats")]
        public List<StatsEntry>? Stats { get; set; }

        public string nickname = "";
        public int age = 0;
        public int starvation = 50;
        public int happiness = 50;
        public bool alive = true;
        public int xp = 0;
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
