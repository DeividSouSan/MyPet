using Tamagotchi;
using RestSharp;
using Newtonsoft.Json;
using System.Timers;

namespace Pokemon
{
    public class PokemonControl
    {
        public static PokemonModel? currentPokemon;
        public static List<string> regions = new() {
            { "kanto" },
            { "johto" },
            { "hoenn" },
            { "sinnoh" },
            { "unova" },
            { "kalos" }
        };

        public static List<string> GetRegionStarter(string region)
        {
            region = region.ToLower();

            Dictionary<string, List<string>> starters = new()
            {
                {"kanto", new List<string> {"Bulbasaur","Squirtle","Charmander"} },
                {"johto", new List<string> {"Totodile","Chicorita","Cyndaquil"} },
                {"hoenn", new List<string> {"Mudkip","Treecko","Torchic"} },
                {"sinnoh",new List<string> { "Piplup","Turtwig","Chimchar"} },
                {"unova", new List<string> {"Oshwatt","Snivy","Tepig"} },
                {"kalos", new List<string> {"Chespin", "Froakie", "Fennekin"}},
            };

            return starters[region];
        }
        public static async Task SearchPokemon(string pokemon)
        {
            var client = new RestClient("https://pokeapi.co/api/v2/");

            var request = new RestRequest($"pokemon/{pokemon}", Method.Get) { Timeout = 200000 };
            var response = await client.GetAsync(request);

            currentPokemon = System.Text.Json.JsonSerializer.Deserialize<PokemonModel>(response.Content!);
        }


        public static void PlayWithPet(int maxHappinessPoint, int maxXpPoint)
        {
            Console.WriteLine("Masocte tal");
            if (currentPokemon!.happiness == 1.0)
            {
                return;
            }
            else if (currentPokemon.happiness > 1.0)
            {
                currentPokemon.happiness = 1;

            }
            else
            {
                Random xpRandom = new();
                int xpPoints = xpRandom.Next(maxXpPoint);
                currentPokemon.xp += xpPoints;

                Random happinessRandom = new();
                int happinessPoints = happinessRandom.Next(maxHappinessPoint);
                currentPokemon.happiness += happinessPoints;
            }
        }

        public static void UpdatePokemonStats(Object source, ElapsedEventArgs e)
        {
            bool pokemonIsDead = currentPokemon!.starvation == 100 | currentPokemon.happiness == 0;
            if (pokemonIsDead)
            {
                File.Delete("pokemonData.json");
                Environment.Exit(0);
            }

            currentPokemon!.starvation += 5;
            currentPokemon.happiness += 5;

            Math.Clamp(currentPokemon.starvation, 0, 100);
            Math.Clamp(currentPokemon.happiness, 0, 100);

            Console.WriteLine("passou 10 segundos");
        }
        public static void SavePokemon(PokemonModel pet)
        {
            var pokemonData = JsonConvert.SerializeObject(pet);
            File.WriteAllText("pokemonData.json", pokemonData);
        }

        public static bool LoadPokemon()
        {
            try
            {
                string dataFromFile = File.ReadAllText("pokemonData.json");
                currentPokemon = JsonConvert.DeserializeObject<PokemonModel>(dataFromFile);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
