using TBAG.Models;

namespace TBAG.Helpers;

public class GameHelper
{
    public static void showLoading()
    {
        Console.Clear();
        Text.WriteGameName();
        Console.Write("Aan het laden");

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Thread.Sleep(500); 
                Console.Write(".");
            }
        
            Thread.Sleep(500); 
            Console.Write("\b\b\b   \b\b\b");
        }

        Console.Clear();
    }
}