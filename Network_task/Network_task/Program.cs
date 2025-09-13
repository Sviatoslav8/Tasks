using Network_task;
using System.Text.Json;

var httpClient = new HttpClient();

httpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/");

Random random = new Random();

while (true)
{
    int num = random.Next(1, 20);

    var response = await httpClient.GetAsync($"pokemon/{num}");

    var content = await response.Content.ReadAsStringAsync();

    var pokemon = JsonSerializer.Deserialize<Pokemon>(content);
    
    Console.WriteLine("enter 1-pokemon\n2-clear screen\n3-exit");
    string choice = Console.ReadLine();
    if (choice == "1")
    {
        Console.WriteLine($"{pokemon.Id} {pokemon.Name} {pokemon.Height} {pokemon.Species.Name}");
        foreach (var item in pokemon.StatsPokemon)
        {
            Console.WriteLine(item.Base_Stats.ToString(), item.Effort.ToString(), item.Stat_Stats.Name);
        }
        foreach (var items in pokemon.HeldItems)
        {                
            Console.WriteLine($"{items.Item.Name}, {items.Item.Url}");
            response = await httpClient.GetAsync($"{items.Item.Url}");
            var responseItem = await httpClient.GetAsync($"{items.Item.Url}");
            var contentItem = await responseItem.Content.ReadAsStringAsync();
            var Item = JsonSerializer.Deserialize<Item>(contentItem);
            Console.WriteLine(Item.Cost.ToString(), Item.Short_Effect);
        }

        
    }
    else if (choice == "2")
    {
        Console.Clear();
    }
    else if (choice == "3")
    {
        break;
    }
    else
        break;
}
