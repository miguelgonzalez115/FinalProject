/*
*******************************************************************
    MIGUEL GONZÁLEZ
    April - 2023
    Final Project for  PROGRAMMING FUNDAMENTALS (CS-1400-001)
    "Inventory system"
*******************************************************************
*/

using System;
using System.Collections.Generic;
using System.IO;


//This Item Class helps create its unique name quantity and price. It has Get and Set to use it outside the class.
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
        //Here just prints on the screen the actual name quantity price and the result of the simple multiplication of the quantity and price
        //So it shows the actual total value.
        Console.WriteLine($"Name: {Name}\nQuantity: {Quantity}\nPrice: ${Price}\nTotal Value: ${Quantity * Price}\n");
    }
}

//This class creates a list of items so it can do one of the most important parts of the code. To modify the list. It can add, remove, edit and display the list.
//It also saves and loads the intentory on a .txt file
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

/*
Here is the main class, Program. Here the program creates the .txt file and if it already exists it just loads it.
*/
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
// This is a inifite while loop, so the menu keeps showing on the console.
//there are 6 options in the menu and the only way to exit the loop is with option 6, which saves the inventory on the file and exit the program.

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
                    //In here the item you want to remove is located on the file.
                    //If the item could not be located, it shows a mesage saying that no item was found in the inventory.
                    List<Item> removeItems = inventory.GetItems().FindAll(i => i.Name == removeName);  
                    if (removeItems.Count == 0)
                    {
                        Console.WriteLine($"No item with the name '{removeName}' found in inventory.");
                    }
                    //Once the program finds the item, it gets removed from the list.
                    else if (removeItems.Count == 1)
                    {
                        inventory.RemoveItem(removeItems[0]);
                        Console.WriteLine($"Item '{removeName}' removed from inventory.");
                    }
                    else
                    //If there are multiples items with the same name, the items are are chosen with numbers depending on the item.
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
                //In this option you can edit the item, you just need the name of the item you want to edit.
                    Console.WriteLine("Please enter the name of the item you want to edit:");
                    string editName = Console.ReadLine();
                    List<Item> editItems = inventory.GetItems().FindAll(i => i.Name == editName);
                    if (editItems.Count == 0)
                    {
                        Console.WriteLine($"No item with the name '{editName}' found in inventory.");
                    }
                    else if (editItems.Count == 1)
                    {
                        //If the item is found, you can input a new name, quantity and price
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
                //This case only displays the current inventory with option 4
                    Console.WriteLine("Current inventory:");
                    inventory.Display();
                    break;
                case "5":
                //Here all the modifications and edits that you have made to the inventory is saved and is safe to exit the program.
                    inventory.Save(fileName);
                    Console.WriteLine($"Inventory saved to file '{fileName}'.");
                    break;
                case "6":
                //In case 6 if you forget to save the inventory and accidentally press 6, it first woul save it and the exit the program.
                    inventory.Save(fileName);
                    Console.WriteLine($"Inventory saved to file '{fileName}'.");
                    Console.WriteLine("Exiting program...");
                    return;
                default:
                //If you input another nombre or a letter it would go to this "default" option, in wich it will show on the console "Invalid input."
                //And it will show the menu again to enter the correct number.
                    Console.WriteLine("Invalid input.");
                    break;
            }

            Console.WriteLine();
        }
    }
}

					