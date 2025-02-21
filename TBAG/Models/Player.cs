namespace TBAG.Models;

public class Player(string username)
{
    private string Username { get; set; } = username;
    private string? LastChoice { get; set; }
    private Dictionary<string, int> Inventory { get; set; } = new Dictionary<string, int>();

    public string GetUsername()
    {
        return Username;
    }
    
    public void AddToInventory(string item, int count)
    {
        Inventory.Add(item, count);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("+ " + count);
        Console.ResetColor();
        Console.WriteLine(" " + item);
    }
    
    public void RemoveFromInventory(string item, int count)
    {
        int amount = Inventory[item];

        if (amount > count)
        {
            int newAmount = amount - count;
            Inventory[item] = newAmount;
        }
        else
        {
            Inventory.Remove(item);
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("- " + count);
        Console.ResetColor();
        Console.WriteLine(" " + item);
    }
    
    public bool HasItem(string item)
    {
        return Inventory.ContainsKey(item);
    }
    
    public void SetLastChoice(string choice)
    {
        LastChoice = choice;
    }
    
    public string? GetLastChoice()
    {
        return LastChoice;
    }
}