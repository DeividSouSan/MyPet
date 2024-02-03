
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
            bool savefileExists = SaveLoadService.LoadPet();

            if (savefileExists)
            {
                Console.Write("Arquivo de Pet encontrado. Carregar? [s/n]");
                string loadSavefile = Console.ReadLine()!.ToLower(); ;

                if (loadSavefile == "s")
                {
                    currentPet = PetController.currentPet!;
                    updatePStatusTimer!.Start();
                }
                else if (loadSavefile == "n")
                {
                    Console.WriteLine("Arquivo de Pet não carregado.");
                }
                else
                {
                    Console.WriteLine("Opção inválida. Arquivo de Pet não carregado.");
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
                1. ➕ | Adotar
                2. 🏠 | Ver Pet
                3. 💬 | Interagir
                4. 👋 | Sair
                >> 
                """);
                string choice = Console.ReadLine()!;

                switch (choice)
                {
                    case "1": await AdoptPet(); break;
                    case "2": ShowPet(); break;
                    case "3": Interact(); break;
                    case "4":
                        SaveLoadService.SavePet(currentPet!);
                        return;
                    default:
                        Console.WriteLine("🚫 Seleção inválida. Tente novamente. 🚫");
                        break;
                }

                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }

        private static async Task AdoptPet()
        {
            // Gets the "pet" name.
            string region, starter;
            try { region = GetRegion(); starter = GetStarter(region); }
            catch (Exception err) { Console.WriteLine($"Erro: {err.Message}! Voltando ao início."); return; };

            // Try to request PokeAPI.
            try { await AdoptionService.SearchPet(starter); }
            catch (Exception err) { Console.WriteLine($"Erro: {err.Message}!"); return; }

            Console.WriteLine($"Pet {starter} adotado com sucesso!");
            currentPet = PetController.currentPet!;

            Console.Write($"Qual o apelido do {starter}: ");
            currentPet.nickname = Console.ReadLine()!.Trim();

            updatePStatusTimer!.Start();
        }

        private static string GetRegion()
        {
            List<string> regions = new() {
                { "kanto" },
                { "johto" },
                { "hoenn" },
                { "sinnoh" },
                { "unova" },
                { "kalos" }
            };

            Console.WriteLine("Regiões disponíveis:  ");
            foreach (var item in regions)
            {
                Console.WriteLine($"- {item[0].ToString().ToUpper()}{item[1..]}");
            }

            Console.Write("Digite o nome da região escolhida: ");
            string region = Console.ReadLine()!.ToLower();

            if (regions.Contains(region)) return region;
            else throw new Exception("Região inválida");
        }

        private static string GetStarter(string region)
        {
            Dictionary<string, List<string>> starters = new()
            {
                {"kanto", new List<string> {"bulbasaur","squirtle","charmander"} },
                {"johto", new List<string> {"totodile","chikorita","cyndaquil"} },
                {"hoenn", new List<string> {"mudkip","treecko","torchic"} },
                {"sinnoh",new List<string> {"piplup","turtwig","chimchar"} },
                {"unova", new List<string> {"oshwatt","snivy","tepig"} },
                {"kalos", new List<string> {"chespin", "froakie", "fennekin"}},
            };

            Console.WriteLine($"Iniciais de {region}:");
            foreach (string starter in starters[region])
            {
                Console.WriteLine($"- {starter[0].ToString().ToUpper()}{starter[1..]}");
            }

            Console.Write("Digite o nome do pet que você quer adotar: ");
            string choosenOne = Console.ReadLine()!.ToLower();

            if (starters[region].Contains(choosenOne)) return choosenOne;
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
            |====================================================
            | NOME                                               
            |----------------------------------------------------
            | {name}                                             
            |====================================================
            | ATRIBUTOS                                          
            |----------------------------------------------------
            | Vida:  {currentPet.Stats![0].BaseStat}             
            | Felicidade: {HappinesStatus(currentPet.happiness)} 
            | Fome: {FoodStatus(currentPet.food)}                
            | Anos de Vida: {currentPet.age}                     
            |====================================================
            | OUTROS                                             
            |----------------------------------------------------
            | Peso: {currentPet.Weight}                          
            | Altura: {currentPet.Height}                        
            |----------------------------------------------------
            """);
        }

        private static string HappinesStatus(int happinessStatus)
        {
            string message = "";
            if (happinessStatus < 20) message = "Muito triste 😭";
            else if (happinessStatus < 40) message = "Triste 😟";
            else if (happinessStatus < 60) message = "Normal 😐";
            else if (happinessStatus < 80) message = "Feliz 😀";
            else message = "Muito Feliz 😄";

            return $"{message} ({happinessStatus})";
        }

        private static string FoodStatus(int foodStatus)
        {
            string message = "";
            if (foodStatus > 80) message = "Cheio 😄";
            else if (foodStatus > 50) message = "Normal 😐";
            else if (foodStatus > 20) message = "Com fome 😟";
            else message = "Faminto 😭";

            return $"{message} ({foodStatus})";
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
            1. 🍕 | Alimentar
            2. 🧼 | Dar banho
            3. 💤 | Colocar para dormir
            4. 🏕️  | Passear no parque
            5. 🧠 | Educar
            6. 🏋️  | Treinar
            """);

            Console.Write(">> ");
            int op = int.Parse(Console.ReadLine()!);
            /*
                        switch (op)
                        {
                            case 1:
                                int food = PetController.InteractionStatusUpdate(currentPet.food);

                                if (food == -1) Console.WriteLine($"O {currentPet.nickname} não está com fome agora!");
                                else Console.WriteLine($"O {currentPet.nickname} recuperou {food} de alimentação.");

                                break;
                            case 4:
                                int happiness = PetController.InteractionStatusUpdate(currentPet.happiness);

                                if (happiness == -1) Console.WriteLine($"O {currentPet.nickname} já está muito feliz!");
                                else Console.WriteLine($"O {currentPet.nickname} ganho {happiness} pontos de felicidade.");

                                break;
                            default:
                                break;
                        }
                        */
        }

        public static void DeathWarning()
        {
            Console.WriteLine($"O {currentPet!.nickname} morreu! Ele vai lembrar de você no céu.");
            File.Delete("petData.json");
            Environment.Exit(0);
        }
    }
}
