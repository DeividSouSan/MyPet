using RestSharp;
using Newtonsoft.Json;
using System.Timers;

namespace Pet
{
    public class PetController
    {
        public static PetModel? currentPet;
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
                {"kanto", new List<string> {"bulbasaur","squirtle","charmander"} },
                {"johto", new List<string> {"totodile","chicorita","cyndaquil"} },
                {"hoenn", new List<string> {"mudkip","treecko","torchic"} },
                {"sinnoh",new List<string> { "Piplup","Turtwig","Chimchar"} },
                {"unova", new List<string> {"oshwatt","snivy","tepig"} },
                {"kalos", new List<string> {"chespin", "froakie", "fennekin"}},
            };

            return starters[region];
        }
        public static async Task SearchPet(string pet)
        {
            var client = new RestClient("https://pokeapi.co/api/v2/");

            var request = new RestRequest($"pokemon/{pet}", Method.Get) { Timeout = 200000 };
            var response = await client.GetAsync(request);

            currentPet = System.Text.Json.JsonSerializer.Deserialize<PetModel>(response.Content!);
        }

        public static void PlayWithPet(int maxHappinessPoint, int maxXpPoint)
        {
            Console.WriteLine("Masocte tal");
            if (currentPet!.happiness == 1.0)
            {
                return;
            }
            else if (currentPet.happiness > 1.0)
            {
                currentPet.happiness = 1;

            }
            else
            {
                Random xpRandom = new();
                int xpPoints = xpRandom.Next(maxXpPoint);
                currentPet.xp += xpPoints;

                Random happinessRandom = new();
                int happinessPoints = happinessRandom.Next(maxHappinessPoint);
                currentPet.happiness += happinessPoints;
            }
        }

        public static void UpdatePokemonStats(Object source, ElapsedEventArgs e)
        {
            bool petIsDead = currentPet!.starvation == 100 | currentPet.happiness == 0;
            if (petIsDead)
            {
                File.Delete("petData.json");
                Environment.Exit(0);
            }

            currentPet!.starvation += 5;
            currentPet.happiness += 5;

            Math.Clamp(currentPet.starvation, 0, 100);
            Math.Clamp(currentPet.happiness, 0, 100);

            Console.WriteLine("passou 10 segundos");
        }
        public static void SavePokemon(PetModel pet)
        {
            var petData = JsonConvert.SerializeObject(pet);
            File.WriteAllText("petData.json", petData);
        }

        public static bool LoadPokemon()
        {
            try
            {
                string dataFromFile = File.ReadAllText("petData.json");
                currentPet = JsonConvert.DeserializeObject<PetModel>(dataFromFile);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
