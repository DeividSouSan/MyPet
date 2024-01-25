using Newtonsoft.Json;
using System.Timers;

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
                {"johto", new List<string> {"totodile","chikorita","cyndaquil"} },
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
        }

        public static void CheckAndHandleDeath()
        {
            if (currentPet!.food <= 0 | currentPet.happiness <= 0)
            {
                currentPet.alive = false;
            }
        }

        public static void UpdatePetStats(Object source, ElapsedEventArgs e)
        {
            // Erros gerados pelos eventos são suprimidos 

            CheckAndHandleDeath();

            Random deprecation = new();
            int deprecationHunger = deprecation.Next(2, 10);

            currentPet!.food -= deprecationHunger;

            Math.Clamp(currentPet.food, 0, 100);
        }

        public static int FeedPet()
        {
            if (currentPet!.food > 80)
            {
                return -1;
            }
            else
            {
                int hunger = 100 - (currentPet!.food - 1);
                Random food = new();

                int foodSatisfaction = food.Next(0, hunger);
                currentPet.food += foodSatisfaction;

                return foodSatisfaction;
            }
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
