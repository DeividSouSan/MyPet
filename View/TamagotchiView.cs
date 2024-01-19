
using Pokemon;
using Tamagotchi;
using System.Timers;

namespace Tamagotchi
{
    public class TamagotchiMenu
    {
        private static string username;
        private static PokemonModel currentPokemon;
        private System.Timers.Timer updatePStatusTimer;

        public TamagotchiMenu()
        {
            updatePStatusTimer = new System.Timers.Timer(10000); // 10 segundos
            updatePStatusTimer.Elapsed += PokemonControl.UpdatePokemonStats;
            updatePStatusTimer.AutoReset = true;
            updatePStatusTimer.Enabled = true;
        }

        public void GetUserName()
        {
            Console.Write("Por favor, digite seu nome: ");
            username = Console.ReadLine()!;
        }

        public void LoadPokemon()
        {
            bool pokemonFileExists = PokemonControl.LoadPokemon();

            if (pokemonFileExists)
            {
                Console.Write("Arquivo de Pokemon encontrado. Carregar? [s/n]");
                string resposta = Console.ReadLine()!;

                if (resposta == "s") currentPokemon = PokemonControl.currentPokemon!;
            }
        }

        public async Task StartGame()
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
                Console.WriteLine($"Olá {username}, escolha uma opção:");
                Console.WriteLine("1. Adotar Tamagotchi");
                Console.WriteLine("2. Ver Tamagotchis");
                Console.WriteLine("3. Interagir com Tamagtchi");
                Console.WriteLine("4.  Sair");

                Console.Write(">> ");
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
                        Interact();
                        break;
                    case "4":
                        var pet = PokemonControl.currentPokemon!;
                        PokemonControl.SavePokemon(pet);
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
            string pokemon;
            try
            {
                Console.WriteLine("Digite o nome da região:  ");
                foreach (var item in PokemonControl.regions) Console.WriteLine($"- {item[0].ToString().ToUpper()}{item.Substring(1)}");

                string region = GetRegion();

                Console.WriteLine($"Iniciais de {region}:");
                foreach (string starter in PokemonControl.GetRegionStarter(region)) Console.WriteLine($"- {starter}");

                pokemon = GetPokemon(region); ;
            }
            catch (Exception err) { Console.WriteLine($"Erro: {err.Message}! Voltando ao início."); return; };

            try
            {
                pokemon = pokemon.ToLower();
                await PokemonControl.SearchPokemon(pokemon);
            }
            catch (Exception err) { Console.WriteLine($"Erro: {err}!"); return; }

            Console.WriteLine($"Pokemon {pokemon} adotado com sucesso!");

            currentPokemon = PokemonControl.currentPokemon!;

            Console.Write($"Qual o apelido do {pokemon}: ");
            currentPokemon.nickname = Console.ReadLine()!;

        }

        public static string GetRegion()
        {
            Console.Write(">> ");
            string region = Console.ReadLine()!.ToLower();

            if (PokemonControl.regions.Contains(region!)) return region;
            else throw new Exception("Região inválida");
        }

        public static string GetPokemon(string region)
        {
            Console.Write("Digite o nome do pokemon que você quer adotar: ");
            string pokemon = Console.ReadLine()!;

            if (PokemonControl.GetRegionStarter(region).Contains(pokemon!)) return pokemon;
            else throw new Exception("Pokemon inválido");
        }
        public static void VerMascote()
        {

            if (currentPokemon == null)
            {
                Console.WriteLine("Você ainda não tem mascote!");
                return;
            }

            Console.WriteLine("Você pode ver todos os Pokemons adotados.");

            if (currentPokemon.nickname == "") Console.WriteLine($"Nome: {currentPokemon.Name?.ToUpper()}");
            else Console.WriteLine($"Nome: {currentPokemon.nickname} ({currentPokemon.Name?.ToUpper()})");

            Console.WriteLine($""" 
            Vida:  {currentPokemon.Stats![0].BaseStat}
            Felicidade: {HappinesStatus(currentPokemon.happiness)} {currentPokemon.happiness}
            Anos de Vida: {currentPokemon.age}

            Peso: {currentPokemon.Weight}
            Altura: {currentPokemon.Height}

            """);
        }



        public static string HappinesStatus(double happinesLevel)
        {
            if (happinesLevel < 0.2) return "Muito triste";
            else if (happinesLevel < 0.4) return "Triste";
            else if (happinesLevel < 0.6) return "Normal";
            else if (happinesLevel < 0.8) return "Feliz";
            else return "Muito Feliz";
        }

        public static void Interact()
        {
            Console.WriteLine($"Escolha uma atividade para fazer com {currentPokemon.nickname}.");

            Console.WriteLine("""
            1 - Ir ao PokePark
            2 - Fazer uma Batalha Pokemon
            3 - Alimentar
            """);

            Console.Write(">> ");
            int op = int.Parse(Console.ReadLine());

            switch (op)
            {
                case 1:
                    break;
                default:
                    break;
            }
        }

    }
}

