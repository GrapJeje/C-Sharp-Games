namespace TBAG.Helpers;

public class Text
{
    public static void WriteGameName()
    {
        int consoleWidth = Console.WindowWidth;
        string fixedEquals = "===";
        string textToDisplay = fixedEquals + " [ TBAG ] ";
        int remainingEqualsCount = consoleWidth - textToDisplay.Length;
        
        remainingEqualsCount = Math.Max(remainingEqualsCount, 0);
        string fullLine = textToDisplay + new string('=', remainingEqualsCount);
        Console.WriteLine(fullLine);
    }

    public static void Narrator(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}