namespace RGP.Model;

public class Game
{
    private static PlayableCharacter playerCharacter;
    private static PlayableCharacter enemyCharacter;
    private bool isPlayerTurn = true;
    
    public Game()
    {
        ChooseCharacter();
        GetRandomCharacter();
        StartGameLoop();
    }
    
    private void StartGameLoop()
    {
        while (playerCharacter.Health > 0 && enemyCharacter.Health > 0)
        {
            Console.Clear();
            Console.WriteLine("=====================================");
            Console.WriteLine($"Speler: {playerCharacter.Character.Name} | Health: {playerCharacter.Health} | Mana: {playerCharacter.Mana}");
            Console.WriteLine($"Vijand: {enemyCharacter.Character.Name} | Health: {enemyCharacter.Health} | Mana: {enemyCharacter.Mana}");
            Console.WriteLine("=====================================");

            if (isPlayerTurn)
            {
                getChooseMenu();
            }
            else
            {
                Thread.Sleep(1000);
                Console.WriteLine("De vijand is aan zet...");
                Thread.Sleep(1500);
                new GameOptions(playerCharacter, enemyCharacter).GetEnemy(this).getRandomOption();
            }
            
            if (playerCharacter.Health <= 0)
            {
                lose();
                break;
            }
            else if (enemyCharacter.Health <= 0)
            {
                win();
                break;
            }

            isPlayerTurn = !isPlayerTurn;
            Thread.Sleep(1000);
        }
    }

    public void ChooseCharacter()
    {
        Console.WriteLine("Kies jouw karakter: \n");
        foreach (var character in Characters.AllCharacters.Values)
        {
            Console.WriteLine(character);
        }
        
        Console.WriteLine("\n Typ het nummer van het karakter dat je wilt kiezen: ");
        int.TryParse(Console.ReadLine(), out int id);
        playerCharacter = new PlayableCharacter(id);
        Console.Clear();
    }

    public void GetRandomCharacter()
    {
        Random random = new Random();
        int randomId;
        do
        {
            randomId = Characters.AllCharacters.Values.ElementAt(random.Next(Characters.AllCharacters.Count)).Id;
        } while (randomId == playerCharacter.Character.Id || randomId == 0);

        enemyCharacter = new PlayableCharacter(randomId);
        
        Console.Write("Je vijand is ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(enemyCharacter.Character.Name + "\n");
        Console.ResetColor();
        Thread.Sleep(2000);
        Console.Clear();
    }

    public void getChooseMenu()
    {
        Console.ResetColor();
        Console.WriteLine("=====================================");
        
        if (playerCharacter.Mana < 10)
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        
        Console.WriteLine("1. Aanvallen (10 mana)");
        
        Console.ResetColor();
        if (playerCharacter.Mana < 25)
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        
        Console.WriteLine("2. block (25 mana)");
        
        Console.ResetColor();
        if (playerCharacter.Mana < 30)
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        Console.WriteLine("3. Heal (30 mana)");
        
        Console.ResetColor();
        Console.WriteLine("4. Overslaan (+15 mana)");
        
        Console.WriteLine("=====================================");
        Console.WriteLine("Je hebt {0} health en {1} mana", playerCharacter.Health, playerCharacter.Mana);
        Console.WriteLine("Welke optie wil je doen?");
        int.TryParse(Console.ReadLine(), out int choose);
        Console.Clear();
        chooseCase(choose);
    }

    public void chooseCase(int input)
    {
        GameOptions GameOptions = new GameOptions(playerCharacter, enemyCharacter);
        switch (input)
        {
            case 1:
                GameOptions.GetPlayer(this).attack();
                break;
            case 2:
                GameOptions.GetPlayer(this).block();
                break;
            case 3:
                GameOptions.GetPlayer(this).heal();
                break;
            case 4:
                GameOptions.GetPlayer(this).skip();
                break;
            default:
                Console.WriteLine("Kies een geldige optie");
                getChooseMenu();
                break;
        }
    }

    public void win()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(@"
 __     ______  _    _  __          _______ _   _  
 \ \   / / __ \| |  | | \ \        / /_   _| \ | | 
  \ \_/ / |  | | |  | |  \ \  /\  / /  | | |  \| | 
   \   /| |  | | |  | |   \ \/  \/ /   | | | . ` | 
    | | | |__| | |__| |    \  /\  /   _| |_| |\  | 
    |_|  \____/ \____/      \/  \/   |_____|_| \_| 
                                                   
");
        Console.ResetColor();
        Console.WriteLine("\nJE HEBT GEWONNEN!\n");
    
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Gefeliciteerd, je hebt de strijd overwonnen!");
        Console.WriteLine("Je bent de ultieme kampioen!");
        Console.ResetColor();
    
        Thread.Sleep(4000);
        Environment.Exit(0);
    }

    public void lose()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(@"
 __     ______  _    _   _      ____   _____ ______ 
 \ \   / / __ \| |  | | | |    / __ \ / ____|  ____|
  \ \_/ / |  | | |  | | | |   | |  | | (___ | |__   
   \   /| |  | | |  | | | |   | |  | |\___ \|  __|  
    | | | |__| | |__| | | |___| |__| |____) | |____ 
    |_|  \____/ \____/  |______\____/|_____/|______|
                                                    
");
        Console.ResetColor();
        Console.WriteLine("\nJE BENT VERSLAGEN!\n");

        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("Je tegenstander was te sterk...");
        Console.ResetColor();
    
        Thread.Sleep(4000);
        Environment.Exit(0);
    }

}