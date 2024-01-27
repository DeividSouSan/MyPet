using Newtonsoft.Json;
using System.Timers;
using Pet;

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
            string response = await client.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{pet}");
            currentPet = System.Text.Json.JsonSerializer.Deserialize<PetModel>(response);
        }

        public static void CheckAndHandleDeath()
        {
            if (currentPet!.food <= 0 | currentPet.happiness <= 0)
            {
                currentPet.alive = false;
                UserInterface.DeathWarning();
            }
        }

        public static void UpdatePetStats(Object source, ElapsedEventArgs e)
        {
            // Erros gerados pelos eventos são suprimidos 

            CheckAndHandleDeath();

            Random deprecation = new();

            currentPet.food -= deprecation.Next(2, 10);
            Math.Clamp(currentPet.food, 0, 100);

            currentPet.happiness -= deprecation.Next(2, 10);
            Math.Clamp(currentPet.happiness, 0, 100);

            DeprecationStatusUpdate("food", {2, 10})

            // Se eu conseguisse acessar um objeto através de uma string acho que daria certo
        }

        public static int DeprecationStatusUpdate(string status, List<int> range)
        {
            // Preciso dar um jeito de conseguir atualizar o status do pokemmon somente com essa função
            Random deprecation = new();
            int min = range[0]
            int max = range[0]
            currentPet.status -= deprecation.Next(min, max)
        }

        public static int InteractionStatusUpdate(int currentStatus)
        {
            // Preciso dar um jeito de conseguir atualizar o status do pokemmon somente com essa função
            if (currentStatus > 80) return -1;
            else
            {
                int rewardAmplitude = 100 - (currentStatus);
                Random random = new();

                if (rewardExtent >= 10) random.Next(10, rewardExtent);
                else 20;
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
