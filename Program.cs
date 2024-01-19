using Pokemon;
using Tamagotchi;

var Menu = new TamagotchiMenu();
Menu.GetUserName();
Menu.LoadPokemon();
await Menu.StartGame();