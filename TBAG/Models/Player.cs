namespace TBAG.Models;

public class Player
{
    private string username { get; set; }
    private string lastChoice { get; set; }
    private Dictionary<string, int> inventory { get; set; } = new Dictionary<string, int>();
    
    public Player(string username)
    {
        this.username = username;
    }
    
    public string GetUsername()
    {
        return username;
    }
    
    public void AddToInventory(string item, int count)
    {
        inventory.Add(item, count);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("+ " + count);
        Console.ResetColor();
        Console.WriteLine(" " + item);
    }
    
    public void RemoveFromInventory(string item, int count)
    {
        int amount = inventory[item];

        if (amount > count)
        {
            int newAmount = amount - count;
            inventory[item] = newAmount;
        }
        else
        {
            inventory.Remove(item);
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("\n+ " + count);
        Console.ResetColor();
        Console.WriteLine(" " + item);
    }
    
    public Dictionary<string, int> GetInventory()
    {
        return inventory;
    }
    
    public void SetLastChoice(string choice)
    {
        lastChoice = choice;
    }
    
    public string GetLastChoice()
    {
        return lastChoice;
    }
}