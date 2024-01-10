using Tamagotchi;
using RestSharp;
using System.Text.Json;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;


namespace Pokemon
{
    public class PokemonControl
    {
        public static PokemonModel? mascote;
        public static string nickname = "";

        public static Dictionary<int, string> regions = new Dictionary<int, string>()
            {
                {1, "Kanto"},
                {2, "Johto"},
                {3, "Hoenn"},
                {4, "Sinnoh"},
                {5, "Unova"},
                {6, "Kalos"},
            };
        public static string ChooseRegionStarter(string region)
        {
            region = region.ToLower();

            Dictionary<string, string> starters = new()
            {
                {"kanto", "1 - Bulbasaur 2 - Squirtle 3 - Charmander"},
                {"johto", "1 - Totodile | 2 - Chicorita | 3 - Cyndaquil"},
                {"hoenn", "1 - Mudkip | 2 - Treecko | 3 - Torchic"},
                {"sinnoh", "1 - Piplup | 2 - Turtwig | 3 - Chimchar"},
                {"unova", "1 - Oshwatt | 2 - Snivy | 3 - Tepig"},
                {"kalos", "1 - Chespin | 2 - Froakie | 3 - Fennekin"},
            };

            return starters[region];
        }
        public static async Task SearchPokemon(string pokemon)
        {
            var client = new RestClient("https://pokeapi.co/api/v2/");

            try
            {
                Console.WriteLine($"Buscando pokemon: {pokemon}");
                var request = new RestRequest($"pokemon/{pokemon}", Method.Get) { Timeout = 5000 };
                var response = await client.GetAsync(request);
                mascote = JsonSerializer.Deserialize<PokemonModel>(response.Content!);
                Console.WriteLine("Pokemon encontrado!");
            }
            catch
            {
                Console.WriteLine("Erro ao acessar a API do Pokemon!");
                throw new Exception("Erro ao acessar a API do Pokemon!");
            }

        }
    }
}