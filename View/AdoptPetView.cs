namespace Pet
{

    class AdoptPetView
    {
        public static async Task AdoptPet()
        {
            // Gets the "pet" name.
            string region, starter;
            try { region = GetRegion(); starter = GetStarter(region); }
            catch (Exception err) { Console.WriteLine($"Erro: {err.Message}! Voltando ao início."); return; };

            // Try to request PokeAPI.
            try { await AdoptionService.SearchPet(starter); }
            catch (Exception err) { Console.WriteLine($"Erro: {err.Message}!"); return; }

            Console.WriteLine($"Pet {starter} adotado com sucesso!");

            Console.Write($"Qual o apelido do {starter}: ");
            PetController.currentPet!.nickname = Console.ReadLine()!.Trim();

            UserInterface.updatePStatusTimer!.Start();
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
    }
}