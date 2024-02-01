namespace Pet
{
    /// <summary>
    /// AdoptionService handles the request and serialization of the pet.
    /// </summary>
    class AdoptionService
    {
        /// <summary>
        /// SearchPet handle the PokéAPI request and the response serialization into PetModel.
        /// </summary>
        /// <param name="pet">The name of the pokémon that will be searched.</param>
        /// <returns>Returns a Task.</returns>
        public static async Task SearchPet(string pet)
        {
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{pet}");
            var currentPet = System.Text.Json.JsonSerializer.Deserialize<PetModel>(response);
            PetController.currentPet = currentPet;
        }
    }
}