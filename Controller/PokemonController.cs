using RestSharp;
using Newtonsoft.Json;
using System.Timers;
using System.Reflection;

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
                {"sinnoh",new List<string> { "piplup","turtwig","chimchar"} },
                {"unova", new List<string> {"oshwatt","snivy","tepig"} },
                {"kalos", new List<string> {"chespin", "froakie", "fennekin"}},
            };

            return starters[region];
        }
        public static async Task SearchPet(string pet)
        {
            var client = new RestClient("https://pokeapi.co/api/v2/");
            var request = new RestRequest($"pokemon/{pet}", Method.Get) { Timeout = 5000 };
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
        // Ineficiente de mais, para manutenção meu deus!
        public static Action UpdateStat(string stat, int tax)
        {
            Dictionary<string, Action> Update = new()
            {
                ["starvation"] = () => currentPet.starvation += tax,
                ["happiness"] = () => currentPet.happiness += tax,
            };

            return Update[stat];
        }


        public static void CheckAndHandleDeath()
        {
            if (currentPet!.starvation == 100 | currentPet.happiness == 0)
            {
                File.Delete("petData.json");
                Environment.Exit(0);
            }
        }
        public static void UpdatePetStats(Object source, ElapsedEventArgs e)
        {
            // Erros gerados pelos eventos são suprimidos 

            CheckAndHandleDeath();

            var updateStarvation = UpdateStat("starvation", 5);
            updateStarvation();
            
            var updateHappiness = UpdateStat("happiness", 5);
            updateHappiness();


            Console.WriteLine("Passou 10s.");
        }
        public static bool SavePokemon(PetModel pet)
        {
            if (pet != null)
            {
                var petData = JsonConvert.SerializeObject(pet);
                File.WriteAllText("petData.json", petData);
                return true;
            }
            else return false;
        }

        public static bool LoadPokemon()
        {
            try
            {
                string dataFromFile = File.ReadAllText("petData.json");
                currentPet = JsonConvert.DeserializeObject<PetModel>(dataFromFile);
                return true;
            }
            catch { return false; }
        }
    }
}
