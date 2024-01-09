
using Pokemon;
using Tamagotchi;

namespace Tamagotchi
{
    public class TamagotchiMenu
    {
        private static string userName = "";

        public void Menu()
        {
            Console.Write("Por favor, digite seu nome: ");
            userName = Console.ReadLine()!;
        }

        public async Task ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("""
            ··················································
            : _____ _   __  __   _   ___  ___   ___ _  _ ___ :
            :|_   _/_\ |  \/  | /_\ / __|/ _ \ / __| || |_ _|:
            :  | |/ _ \| |\/| |/ _ \ (_ | (_) | (__| __ || | :
            :  |_/_/ \_\_|  |_/_/ \_\___|\___/ \___|_||_|___|:
            ··················································
            """);
                Console.WriteLine($"Olá {userName}, escolha uma opção:");
                Console.WriteLine("1. Adotar Tamagotchi");
                Console.WriteLine("2. Ver Tamagotchis");
                Console.WriteLine("3. Interagir com Tamagtchi");
                Console.WriteLine("4.  Sair");

                string choice = Console.ReadLine()!;

                switch (choice)
                {
                    case "1":
                        await AdotarMascote();
                        break;
                    case "2":
                        VerMascote();
                        break;
                    case "3":
                        Console.WriteLine("Interagir.");
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Seleção inválida. Tente novamente.");
                        break;
                }

                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }

        public static async Task AdotarMascote()
        {
            string region = PokemonControl.ChooseRegion();
            Console.WriteLine("Você pode adotar qualquer Pokemons.");
            string pokemon = PokemonControl.GetPokemonName();

            try
            {
                await PokemonControl.SearchPokemon(pokemon);
                
                Console.WriteLine($"Pokemon {pokemon} adotado com sucesso!");

                Console.Write($"Qual o apelido do {pokemon}: ");
                PokemonControl.nickname = Console.ReadLine()!;
            }
            catch
            {
                Console.WriteLine("Erro ao adotar o Pokemon!");
            }
        }

        public static void VerMascote()
        {
            Console.WriteLine("Você pode ver todos os Pokemons adotados.");
            PokemonControl.ShowPokemon();
        }
    }
}