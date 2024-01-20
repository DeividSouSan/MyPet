
namespace Pet
{
    public class UserInterface
    {
        private static string? username;
        private static PetModel? currentPet;
        private readonly System.Timers.Timer updatePStatusTimer;

        private UserInterface()
        {
            updatePStatusTimer = new System.Timers.Timer(10000); // 10 segundos
            updatePStatusTimer.Elapsed += PetController.UpdatePokemonStats!;
            updatePStatusTimer.AutoReset = true;
            updatePStatusTimer.Enabled = true;
        }

        public void GetUserName()
        {
            Console.Write("Por favor, digite seu nome: ");
            username = Console.ReadLine()!;
        }

        public void LoadPet()
        {
            bool saveFileExists = PetController.LoadPokemon();

            if (saveFileExists)
            {
                Console.Write("Arquivo de Pet encontrado. Carregar? [s/n]");
                string ans = Console.ReadLine()!;

                if (ans == "s") currentPet = PetController.currentPet!;
            }
        }

        public async Task StartGame()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("""
            .----------------------------.
            | __  __       ____      _   |
            ||  \/  |_   _|  _ \ ___| |_ |
            || |\/| | | | | |_) / _ \ __||
            || |  | | |_| |  __/  __/ |_ |
            ||_|  |_|\__, |_|   \___|\__||
            |        |___/               |
            '----------------------------'
            """);
                Console.WriteLine($"Olá {username}, escolha uma opção:");
                Console.WriteLine("1. Adotar Pet");
                Console.WriteLine("2. Ver Tamagotchis");
                Console.WriteLine("3. Interagir com Tamagtchi");
                Console.WriteLine("4.  Sair");

                Console.Write(">> ");
                string choice = Console.ReadLine()!;

                switch (choice)
                {
                    case "1":
                        await AdoptPet();
                        break;
                    case "2":
                        ShowPet();
                        break;
                    case "3":
                        Interact();
                        break;
                    case "4":
                        var pet = PetController.currentPet!;
                        PetController.SavePokemon(pet);
                        return;
                    default:
                        Console.WriteLine("Seleção inválida. Tente novamente.");
                        break;
                }

                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }

        private static async Task AdoptPet()
        {
            string? pet;

            try
            {
                string region = GetRegion();
                pet = GetPet(region); ;
            }
            catch (Exception err) { Console.WriteLine($"Erro: {err.Message}! Voltando ao início."); return; };

            try
            {
                Console.WriteLine(pet);
                await PetController.SearchPet(pet);
            }
            catch (Exception err)
            {
                Console.WriteLine($"Erro: {err.Message}!");
                Console.Write("Continuar tentando? [s/n]: ");
            }

            Console.WriteLine($"Pet {pet} adotado com sucesso!");

            currentPet = PetController.currentPet!;

            Console.Write($"Qual o apelido do {pet}: ");
            currentPet.nickname = Console.ReadLine()!;
        }

        private static string GetRegion()
        {
            Console.WriteLine("Digite o nome da região:  ");
            foreach (var item in PetController.regions) Console.WriteLine($"- {item[0].ToString().ToUpper()}{item[1..]}");

            Console.Write(">> ");
            string region = Console.ReadLine()!.ToLower();

            if (PetController.regions.Contains(region!)) return region;
            else throw new Exception("Região inválida");
        }

        private static string GetPet(string region)
        {
            Console.WriteLine($"Iniciais de {region}:");
            foreach (string starter in PetController.GetRegionStarter(region))
            {
                Console.WriteLine($"- {starter[0].ToString().ToUpper()}{starter[1..]}");
            }
            Console.Write("Digite o nome do pet que você quer adotar: ");
            string pet = Console.ReadLine()!.ToLower();

            if (PetController.GetRegionStarter(region).Contains(pet)) return pet;
            else throw new Exception("Pet inválido");
        }
        private static void ShowPet()
        {
            if (currentPet == null)
            {
                Console.WriteLine("Você ainda não tem mascote!");
                return;
            }

            Console.WriteLine("Você pode ver todos os Pokemons adotados.");

            string nickname = currentPet!.nickname;
            string species = currentPet.Name!;

            //if (currentPet!.nickname == "") Console.WriteLine($"Nome: {currentPet.Name?.ToUpper()}");
            //else Console.WriteLine($"Nome: {currentPet.nickname} ({currentPet.Name?.ToUpper()})");

            string name = nickname != null ? $"{nickname} ({species})" : $"{species}";
            Console.WriteLine($""" 
            Nome: {name}
            Vida:  {currentPet.Stats![0].BaseStat}
            Felicidade: {HappinesStatus(currentPet.happiness)} {currentPet.happiness}
            Anos de Vida: {currentPet.age}

            Peso: {currentPet.Weight}
            Altura: {currentPet.Height}

            """);
        }
        private static string HappinesStatus(double happinesLevel)
        {
            if (happinesLevel < 0.2) return "Muito triste";
            else if (happinesLevel < 0.4) return "Triste";
            else if (happinesLevel < 0.6) return "Normal";
            else if (happinesLevel < 0.8) return "Feliz";
            else return "Muito Feliz";
        }

        private static void Interact()
        {
            Console.WriteLine($"Escolha uma atividade para fazer com {currentPet.nickname}.");

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

