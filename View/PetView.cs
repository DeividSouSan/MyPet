
namespace Pet
{
    public class UserInterface
    {
        private static string? username;
        private static PetModel? pet;
        public static System.Timers.Timer? updatePStatusTimer;

        private UserInterface()
        {
            updatePStatusTimer = new System.Timers.Timer(10000); // 10 segundos
            updatePStatusTimer.Elapsed += PetController.UpdatePetStats!;
            updatePStatusTimer.AutoReset = true;
            updatePStatusTimer.Enabled = false;
        }

        private void GetUserName()
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
                    pet = PetController.currentPet!;
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
                    case "1": await AdoptPetView.AdoptPet(); break;
                    case "2": ShowPetView.ShowPet(); break;
                    case "3": InteractionPetView.Interact(); break;
                    case "4":
                        SaveLoadService.SavePet(pet!);
                        return;
                    default:
                        Console.WriteLine("🚫 Seleção inválida. Tente novamente. 🚫");
                        break;
                }

                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }

        public static void DeathWarning()
        {
            Console.WriteLine($"O {pet!.nickname} morreu! Ele vai lembrar de você no céu.");
            File.Delete("savefile.json");
            Environment.Exit(0);
        }
    }
}
