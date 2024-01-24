using RestSharp;
using Newtonsoft.Json;
using System.Timers;
using System.Net.Http;

namespace Pet
{
    public class PetController
    {
        public static PetModel? currentPet;

        public static List<string> GetPet(string option, string region = "")
        {
            List<string> regions = new() {
                { "kanto" },
                { "johto" },
                { "hoenn" },
                { "sinnoh" },
                { "unova" },
                { "kalos" }
            };

            Dictionary<string, List<string>> starters = new()
            {
                {"kanto", new List<string> {"bulbasaur","squirtle","charmander"} },
                {"johto", new List<string> {"totodile","chicorita","cyndaquil"} },
                {"hoenn", new List<string> {"mudkip","treecko","torchic"} },
                {"sinnoh",new List<string> { "piplup","turtwig","chimchar"} },
                {"unova", new List<string> {"oshwatt","snivy","tepig"} },
                {"kalos", new List<string> {"chespin", "froakie", "fennekin"}},
            };

            if (option == "regions") return regions;
            else if (option == "starters") return starters[region];
            else throw new Exception("Erro: pipopi");

        }
        
        public static async Task SearchPet(string pet)
        {
                HttpClient client = new HttpClient();
                Console.WriteLine("passou 1");
                string response = await client.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{pet}");
                Console.WriteLine("passou 2");
                currentPet = System.Text.Json.JsonSerializer.Deserialize<PetModel>(response);
                Console.WriteLine("passou 3");
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

        public static void CheckAndHandleDeath()
        {
            if (currentPet!.hunger == 100 | currentPet.happiness == 0)
            {
                File.Delete("petData.json");
                Environment.Exit(0);
            }
        }
        
        public static void UpdatePetStats(Object source, ElapsedEventArgs e)
        {
            // Erros gerados pelos eventos são suprimidos 

            CheckAndHandleDeath();

            currentPet!.hunger += 5;
            currentPet!.happiness -= 5;
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
