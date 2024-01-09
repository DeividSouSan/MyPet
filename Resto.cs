/* // See https://aka.ms/new-console-template for more information
using System.Text.Json;
using RestSharp;
using System.Text;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Tamagochi.Mascote;
using System.Net;
using MenuController;

bool menuIsRunning = true;

Menu.GetUsername();

while (menuIsRunning) {
    int opcao = Menu.ShowMenu();
    Console.WriteLine(opcao);
    switch (opcao)
    {
        case 1:
            Console.Clear();
            Console.WriteLine("Adotar um mascote virtual");
            break;
        case 2:
            Console.Clear();
            Console.WriteLine("Ver meus mascotes");
            break;
        case 3:
            Console.Clear();
            Console.WriteLine("Sair");
            menuIsRunning = false;
            break;
        default:
            Console.Clear();
            Console.WriteLine("Opção inválida");
            break;
    }
}

Console.Write("Digite o nome do pokemon: ");
string pokemon = Console.ReadLine()!.ToLower();

var client = new RestClient("https://pokeapi.co/api/v2/");
var request = new RestRequest($"pokemon/{pokemon}", Method.Get) { Timeout = 300000 };

Console.WriteLine($"Buscando pokemon: {pokemon}");
var response = await client.GetAsync(request);
var mascote = JsonSerializer.Deserialize<Mascote>(response.Content!);

Console.WriteLine($"Nome: {mascote?.Name?.ToUpper()}");
Console.WriteLine("Habilidades:");
foreach (var abilityEntry in mascote?.Abilities!)
{
    Console.WriteLine($"- {abilityEntry?.Ability?.Name?.ToUpper()}");
}
Console.WriteLine("Status:");
foreach (var statsEntry in mascote?.Stats!)
{
    Console.WriteLine($"- {statsEntry?.Stat?.Name?.ToUpper()} = {statsEntry?.BaseStat}");
}



namespace MenuController
{

    public class Menu
    {
        public static string username = "";
        public static string menuMessage = """
··················································
: _____ _   __  __   _   ___  ___   ___ _  _ ___ :
:|_   _/_\ |  \/  | /_\ / __|/ _ \ / __| || |_ _|:
:  | |/ _ \| |\/| |/ _ \ (_ | (_) | (__| __ || | :
:  |_/_/ \_\_|  |_/_/ \_\___|\___/ \___|_||_|___|:
··················································

Bem vindo, {username}!
O que deseja fazer?

1 - Adotar um mascote virtual
2 - Ver meus mascotes
3 - Sair
""";
        public static void GetUsername()
        {
            Console.Clear();
            Console.Write("Bem vindo ao Tamagochi! Para continuar digite seu nome: ");
            username = Console.ReadLine()!;
        }
        public static int ShowMenu()
        {
            Console.WriteLine(menuMessage);

            Console.Write("Digite a opção desejada: ");
            int opcao = int.Parse(Console.ReadLine()!);
            return opcao;
        }
    }
} */