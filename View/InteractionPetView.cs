namespace Pet
{

    public class InteractionPetView
    {
        public static PetModel? pet;
        public static void Interact()
        {
            if (pet == null)
            {
                Console.WriteLine("Você ainda não possui pet. Adote um primeiro!");
                return;
            }

            Console.WriteLine($"Escolha uma atividade para fazer com {pet.nickname}.");

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
        }
    }
}
