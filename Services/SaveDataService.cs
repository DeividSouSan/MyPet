using Newtonsoft.Json;

namespace Pet
{
    /// <summary>
    /// SaveLoadService saves and load de PetModel attributes.
    /// </summary>
    class SaveLoadService
    {
        /// <summary>
        /// SavePet saves the data into savefile.json.
        /// </summary>
        /// <param name="pet">The PetModel object with the attributes to be saved.</param>
        /// <returns>Boolean.</returns>
        public static bool SavePet(PetModel pet)
        {
            if (pet != null)
            {
                var petData = JsonConvert.SerializeObject(pet);
                File.WriteAllText("savefile.json", petData);
                return true;
            }
            else return false;
        }

        /// <summary>
        /// LoadPet reads the savename.json and deserialize it into PetModel PetController.currentPet.
        /// </summary>
        /// <returns>Boolean.</returns>
        public static bool LoadPet()
        {
            try
            {
                string dataFromFile = File.ReadAllText("savefile.json");
                PetController.currentPet = JsonConvert.DeserializeObject<PetModel>(dataFromFile);
                return true;
            }
            catch { return false; }
        }
    }
}