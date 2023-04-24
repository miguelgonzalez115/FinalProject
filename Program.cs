/*

MIGUEL GONZALEZ
April - 2023
Final Project for  PROGRAMMING FUNDAMENTALS (CS-1400-001)
    "Inventory system"

*/

using System;
using System.Collections.Generic;
using System.IO;

class Item
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }

    public Item(string name, int quantity, double price)
    {
        Name = name;
        Quantity = quantity;
        Price = price;
    }

    public void Display()
    {
        Console.WriteLine($"Name: {Name}\nQuantity: {Quantity}\nPrice: ${Price}\nTotal Value: ${Quantity * Price}\n");
    }
}

class Inventory
{
    private List<Item> items = new List<Item>();

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    public void EditItem(Item item, string newName, int newQuantity, double newPrice)
    {
        item.Name = newName;
        item.Quantity = newQuantity;
        item.Price = newPrice;
    }

    public void Display()
    {
        foreach (var item in items)
        {
            item.Display();
        }
    }

    public List<Item> GetItems()
    {
        return items;
    }

    public void Save(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (var item in items)
            {
                writer.WriteLine($"{item.Name},{item.Quantity},{item.Price}");
            }
        }
    }

    public void Load(string fileName)
    {
        using (StreamReader reader = new StreamReader(fileName))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                string name = parts[0];
                int quantity = int.Parse(parts[1]);
                double price = double.Parse(parts[2]);
                Item item = new Item(name, quantity, price);
                AddItem(item);
            }
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        Inventory inventory = new Inventory();
        string fileName = "inventory.txt";

        if (File.Exists(fileName))
        {
            inventory.Load(fileName);
        }

        while (true)
        {
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. Add an item");
            Console.WriteLine("2. Remove an item");
            Console.WriteLine("3. Edit an item");
            Console.WriteLine("4. Display inventory");
            Console.WriteLine("5. Save inventory");
            Console.WriteLine("6. Exit");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.WriteLine("Please enter the name of the item:");
                    string name = Console.ReadLine();
                    Console.WriteLine("Please enter the quantity of the item:");
                    int quantity = int.Parse(Console.ReadLine());
                    Console.WriteLine("Please enter the price of the item:");
                    double price = double.Parse(Console.ReadLine());
                    Item item = new Item(name, quantity, price);
                    inventory.AddItem(item);
                    break;
                case "2":
                    Console.WriteLine("Please enter the name of the item you want to remove:");
                    string removeName = Console.ReadLine();
                    List<Item> removeItems = inventory.GetItems().FindAll(i => i.Name == removeName);
                    if (removeItems.Count == 0)
                    {
                        Console.WriteLine($"No item with the name '{removeName}' found in inventory.");
                    }
                    else if (removeItems.Count == 1)
                    {
                        inventory.RemoveItem(removeItems[0]);
                        Console.WriteLine($"Item '{removeName}' removed from inventory.");
                    }
                    else
                     {
                        Console.WriteLine($"Multiple items found with the name '{removeName}'. Please choose the index of the item you want to remove:");
                        for (int i = 0; i < removeItems.Count; i++)
                        {
                            Console.WriteLine($"{i}. {removeItems[i].Name} ({removeItems[i].Quantity} x ${removeItems[i].Price})");
                        }
                        int removeIndex = int.Parse(Console.ReadLine());
                        inventory.RemoveItem(removeItems[removeIndex]);
                        Console.WriteLine($"Item '{removeName}' removed from inventory.");
                    }
                    break;
                case "3":
                    Console.WriteLine("Please enter the name of the item you want to edit:");
                    string editName = Console.ReadLine();
                    List<Item> editItems = inventory.GetItems().FindAll(i => i.Name == editName);
                    if (editItems.Count == 0)
                    {
                        Console.WriteLine($"No item with the name '{editName}' found in inventory.");
                    }
                    else if (editItems.Count == 1)
                    {
                        Console.WriteLine($"Editing item '{editName}'...");
                        Console.WriteLine("Please enter the new name of the item:");
                        string newName = Console.ReadLine();
                        Console.WriteLine("Please enter the new quantity of the item:");
                        int newQuantity = int.Parse(Console.ReadLine());
                        Console.WriteLine("Please enter the new price of the item:");
                        double newPrice = double.Parse(Console.ReadLine());
                        inventory.EditItem(editItems[0], newName, newQuantity, newPrice);
                        Console.WriteLine($"Item '{editName}' updated in inventory.");
                    }
                    else
                    {
                        Console.WriteLine($"Multiple items found with the name '{editName}'. Please choose the index of the item you want to edit:");
                        for (int i = 0; i < editItems.Count; i++)
                        {
                            Console.WriteLine($"{i}. {editItems[i].Name} ({editItems[i].Quantity} x ${editItems[i].Price})");
                        }
                        int editIndex = int.Parse(Console.ReadLine());
                        Console.WriteLine($"Editing item '{editName}'...");
                        Console.WriteLine("Please enter the new name of the item:");
                        string newName = Console.ReadLine();
                        Console.WriteLine("Please enter the new quantity of the item:");
                        int newQuantity = int.Parse(Console.ReadLine());
                        Console.WriteLine("Please enter the new price of the item:");
                        double newPrice = double.Parse(Console.ReadLine());
                        inventory.EditItem(editItems[editIndex], newName, newQuantity, newPrice);
                        Console.WriteLine($"Item '{editName}' updated in inventory.");
                    }
                    break;
                case "4":
                    Console.WriteLine("Current inventory:");
                    inventory.Display();
                    break;
                case "5":
                    inventory.Save(fileName);
                    Console.WriteLine($"Inventory saved to file '{fileName}'.");
                    break;
                case "6":
                    inventory.Save(fileName);
                    Console.WriteLine($"Inventory saved to file '{fileName}'.");
                    Console.WriteLine("Exiting program...");
                    return;
                default:
                    Console.WriteLine("Invalid input.");
                    break;
            }

            Console.WriteLine();
        }
    }
}

					