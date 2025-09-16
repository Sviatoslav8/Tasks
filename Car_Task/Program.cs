using BLL.Models;
using Car_Task.Services;
using DLL;
using DLL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.IdentityModel.Protocols;
using System.Runtime.CompilerServices;

string connectionstring = "";
var optionBuilder = new DbContextOptionsBuilder<DataContext>()
    .UseSqlServer(connectionstring);

var context = new DataContext(optionBuilder.Options);

var repository = new CarRepository(context);

var service = new CarService(context, repository);


while (true)
{
    Console.WriteLine("Enter number\n1 - add car\n2 - remove car\n 3 - update car\n4 - get all car\n5 - exit");
    string userchoice = Console.ReadLine();
    if(userchoice == "1")
    {
        Console.WriteLine("Enter name - ");
        string name = Console.ReadLine();
        Console.WriteLine("Enter models - ");
        string models = Console.ReadLine();
        Console.WriteLine("Enter color - ");
        string color = Console.ReadLine();
        Console.WriteLine("Enter price - ");
        string price = Console.ReadLine();
        int priceCar = Int32.Parse(price);
        Car car = new Car()
        {
            Name = name,
            Models = models,
            Color = color,
            Price = priceCar
        };
        service.AddAsync(car);
    }else if (userchoice == "2")
    {
        Console.WriteLine("Enter name - ");
        string name = Console.ReadLine();
        Console.WriteLine("Enter models - ");
        string models = Console.ReadLine();
        Console.WriteLine("Enter color - ");
        string color = Console.ReadLine();
        Console.WriteLine("Enter price - ");
        string price = Console.ReadLine();
        int priceCar = Int32.Parse(price);
        Car car = new Car()
        {
            Name = name,
            Models = models,
            Color = color,
            Price = priceCar
        };
        service.Remove(car);
    }
    else if(userchoice == "4")
    {
        await service.GetCarsAsync();
    }else if(userchoice == "5")
    {
        break;
    }
}