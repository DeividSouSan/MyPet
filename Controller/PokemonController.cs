using Tamagotchi;
using RestSharp;
using System.Text.Json;


namespace Pokemon
{
    public class PokemonControl
    {
        public static PokemonModel? mascote;
        public static string GetPokemonName()
        {
            Console.Write("Digite o nome do pokemon: ");
            string pokemon = Console.ReadLine()!.ToLower();
            return pokemon;
        }
        public static async Task SearchPokemon(string pokemon)
        {
            var client = new RestClient("https://pokeapi.co/api/v2/");

            try
            {
                Console.WriteLine($"Buscando pokemon: {pokemon}");
                var request = new RestRequest($"pokemon/{pokemon}", Method.Get) {Timeout = 5000};
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

        public static void ShowPokemon()
        {
            Console.WriteLine($"Nome: {mascote?.Name?.ToUpper()}");
            Console.WriteLine("Habilidades:");
            foreach (var abilityEntry in mascote?.Abilities!)
            {
                Console.WriteLine($"- {abilityEntry?.Ability?.Name?.ToUpper()}");
            }
            Console.WriteLine("Status:");
            foreach (var statsEntry in mascote?.Stats!)
            {
                Console.WriteLine($"- {statsEntry?.Stat?.Name?.ToUpper()} = {statsEntry?.BaseStat}");
            }
        }


    }
}