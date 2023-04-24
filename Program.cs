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


					