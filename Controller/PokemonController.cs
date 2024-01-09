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
        public static string GetPokemonName()
        {
            Console.Write("Digite o nome do pokemon: ");
            string pokemon = Console.ReadLine()!.ToLower();
            return pokemon;
        }

        public static string ChooseRegion()
        {
            Dictionary<int, string> regions = new Dictionary<int, string>()
            {
                {1, "Kanto"},
                {2, "Johto"},
                {3, "Hoenn"},
                {4, "Sinnoh"},
                {5, "Unova"},
                {6, "Kalos"},
            };

            Console.WriteLine("Escolha uma região: ");
            foreach (var item in regions)
            {
                Console.WriteLine($"{item.Key} - {item.Value}");
            }

            string region = Console.ReadLine()!;
            return region;
        }

        public static string ChooseRegionInitial(string region)
        {
            region = region.ToLower();

            Dictionary<string, string> regions = new Dictionary<string, string>()
            {
                {"kanto", "1 - Bulbasaur 2 - Squirtle 3 - Charmander"},
                {"johto", "Johto"},
                {"hoenn", "Hoenn"},
                {4, "Sinnoh"},
                {5, "Unova"},
                {6, "Kalos"},
            };

            return regions[region];
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

        public static void ShowPokemon()
        {
            if (nickname == "") Console.WriteLine($"Nome: {mascote?.Name?.ToUpper()}");
            else Console.WriteLine($"Nome: {nickname} ({mascote?.Name?.ToUpper()})");

            Console.WriteLine($""" 
            Peso: {mascote!.Weight}
            Altura: {mascote!.Height}
            {mascote!.Stats![0].Stat!.Name!.ToUpper()}:  {mascote!.Stats![0].BaseStat}
            """);
        }


    }
}