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


					