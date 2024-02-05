namespace Pet
{
    public class ShowPetView
    {
        public static PetModel? pet;
        private ShowPetView()
        {
            var pet = PetController.currentPet;
        }

        public static void ShowPet()
        {
            if (pet == null)
            {
                Console.WriteLine("Você ainda não tem mascote!");
                return;
            }

            string nickname = pet.nickname;
            string species = pet.Name!;

            string HappinessEmoji = StatusEmojiCalculator.HappinessStatus(pet.happiness);

            string FoodEmoji = StatusEmojiCalculator.FoodStatus(pet.food);

            string name = nickname != "" ? $"{nickname} ({species})" : $"{species}";

            Console.WriteLine($""" 
            |====================================================
            | NOME                                               
            |----------------------------------------------------
            | {name}                                             
            |====================================================
            | ATRIBUTOS                                          
            |----------------------------------------------------
            | Vida:  {pet.Stats![0].BaseStat}             
            | Felicidade: {HappinessEmoji} 
            | Fome: {FoodEmoji}                
            | Anos de Vida: {pet.age}                     
            |====================================================
            | OUTROS                                             
            |----------------------------------------------------
            | Peso: {pet.Weight}                          
            | Altura: {pet.Height}                        
            |----------------------------------------------------
            """);
        }
    }
}
