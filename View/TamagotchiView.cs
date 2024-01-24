
namespace Pet
{
    public class UserInterface
    {
        private static string? username;
        private static PetModel? currentPet = null;
        private static System.Timers.Timer? updatePStatusTimer;

        public UserInterface()
        {
            updatePStatusTimer = new System.Timers.Timer(10000); // 10 segundos
            updatePStatusTimer.Elapsed += PetController.UpdatePetStats!;
            updatePStatusTimer.AutoReset = true;
            updatePStatusTimer.Enabled = false;
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
                string retrieveSaveFile = Console.ReadLine()!;

                if (retrieveSaveFile == "s")
                {
                    currentPet = PetController.currentPet!;
                    updatePStatusTimer!.Start();
                }
            }
        }
        public async Task StartGame()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(
                """
                .----------------------------.
                | __  __       ____      _   |
                ||  \/  |_   _|  _ \ ___| |_ |
                || |\/| | | | | |_) / _ \ __||
                || |  | | |_| |  __/  __/ |_ |
                ||_|  |_|\__, |_|   \___|\__||
                |        |___/               |
                '----------------------------'
                """);
                Console.Write($"""
                Olá {username}, escolha uma opção:
                1. Adotar Pet
                2. Ver Tamagotchis
                3. Interagir com Tamagtchi
                4. Sair
                >> 
                """);
                string choice = Console.ReadLine()!;

                switch (choice)
                {
                    case "1": await AdoptPet(); break;
                    case "2": ShowPet(); break;
                    case "3": Interact(); break;
                    case "4":
                        PetController.SavePokemon(currentPet!);
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
            string? pet, region;

            try { region = GetRegion(); pet = GetPet(region); }
            catch (Exception err) { Console.WriteLine($"Erro: {err.Message}! Voltando ao início."); return; };

            try { await PetController.SearchPet(pet); }
            catch (Exception err) { Console.WriteLine($"Erro: {err.Message}!"); return;}

            Console.WriteLine($"Pet {pet} adotado com sucesso!");
            currentPet = PetController.currentPet!;

            Console.Write($"Qual o apelido do {pet}: ");
            currentPet.nickname = Console.ReadLine()!.Trim();

            updatePStatusTimer!.Start();
        }

        private static string GetRegion()
        {
            Console.WriteLine("Regiões disponíveis:  ");
            foreach (var item in PetController.GetPet("regions"))
            {
                Console.WriteLine($"- {item[0].ToString().ToUpper()}{item[1..]}");
            }

            Console.Write("Digite o nome da região escolhida: ");
            string region = Console.ReadLine()!.ToLower();

            if (PetController.GetPet("regions").Contains(region)) return region;
            else throw new Exception("Região inválida");
        }

        private static string GetPet(string region)
        {
            Console.WriteLine($"Iniciais de {region}:");
            foreach (string starter in PetController.GetPet("starters", region))
            {
                Console.WriteLine($"- {starter[0].ToString().ToUpper()}{starter[1..]}");
            }

            Console.Write("Digite o nome do pet que você quer adotar: ");
            string pet = Console.ReadLine()!.ToLower();

            if (PetController.GetPet("starters", region).Contains(pet)) return pet;
            else throw new Exception("Pet inválido");
        }
        
        private static void ShowPet()
        {
            if (currentPet == null)
            {
                Console.WriteLine("Você ainda não tem mascote!");
                return;
            }

            string nickname = currentPet!.nickname;
            string species = currentPet.Name!;

            string name = nickname != "" ? $"{nickname} ({species})" : $"{species}";

            Console.WriteLine($""" 
            ==========================================================================
            NOME
            --------------------------------------------------------------------------
            {name}
            ==========================================================================
            ATRIBUTOS
            --------------------------------------------------------------------------
            Vida:  {currentPet.Stats![0].BaseStat}
            Felicidade: {HappinesStatus(currentPet.happiness)} {currentPet.happiness}
            Fome: {HungerStatus(currentPet.hunger)} {currentPet.happiness}
            Anos de Vida: {currentPet.age}
            ==========================================================================
            OUTROS
            --------------------------------------------------------------------------
            Peso: {currentPet.Weight}
            Altura: {currentPet.Height}
            --------------------------------------------------------------------------
            """);
        }

        private static string HappinesStatus(int happinesLevel)
        {
            if (happinesLevel < 20) return "Muito triste";
            else if (happinesLevel < 40) return "Triste";
            else if (happinesLevel < 60) return "Normal";
            else if (happinesLevel < 80) return "Feliz";
            else return "Muito Feliz";
        }

        private static string HungerStatus(int starvationLevel)
        {
            if (starvationLevel < 50) return "Sem fome";
            else if (starvationLevel < 80) return "Com fome";
            else return "Faminto";
        }
        
        private static void Interact()
        {
            if (currentPet == null)
            {
                Console.WriteLine("Você ainda não possui pet. Adote um primeiro!");
                return;
            }


            Console.WriteLine($"Escolha uma atividade para fazer com {currentPet.nickname}.");

            Console.WriteLine("""
            1 - Ir ao PokePark
            2 - Fazer uma Batalha Pokemon
            3 - Alimentar
            """);

            Console.Write(">> ");
            int op = int.Parse(Console.ReadLine()!);

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
