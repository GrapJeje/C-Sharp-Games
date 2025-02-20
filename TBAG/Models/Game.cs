using TBAG.Helpers;

namespace TBAG.Models;

public class Game
{
    private bool isRunning = true;
    private Player player { get; set; }
    
    public Game()
    {
        register();
        writeStartStory();
        
        while (isRunning)
        {
            question1();
            question2();
            question3();
        }
    }

    private void register()
    {
        string? username = null;
        while (username == null || username.Length < 3)
        {
            Console.Clear();
            Text.WriteGameName();
            
            if (username != null && username.Length < 3)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Username moet minimaal 3 karakters bevatten.");
                Console.ResetColor();
            }
            
            Console.WriteLine("Vul een username in:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            username = Console.ReadLine();
            Console.ResetColor();
        }
        player = new Player(username);
    }

    private void writeStartStory()
    {
        GameHelper.showLoading();
        
        Text.WriteGameName();
        Thread.Sleep(1000);
        Text.Narrator("Je opent een oude, vergeelde kaart die je op zolder hebt gevonden...");
        Thread.Sleep(3500);

        Text.Narrator("Volgens de kaart ligt er een verborgen schat diep in de ruïnes van Eldoria.");
        Thread.Sleep(3500);

        Text.Narrator("Niemand die daar ooit heen is gegaan, is ooit teruggekeerd...");
        Thread.Sleep(4000);

        Text.Narrator("Toch voel je een onweerstaanbare drang om het avontuur aan te gaan.");
        Thread.Sleep(3500);

        Text.Narrator("\nJe pakt je rugzak en stapt de donkere wildernis in...");
        Thread.Sleep(4000);

        Text.Narrator("\nHet avontuur begint nu!");
        Thread.Sleep(1500);
        
        player.AddToInventory("Backpack", 1);
        Thread.Sleep(1500);
    }
    
    private void question1()
    {
        GameHelper.showLoading();
        Console.Clear();
        Text.WriteGameName();
        Thread.Sleep(1000);
        
        Text.Narrator("Na een lange tocht door het dichte woud kom je bij een splitsing...");
        Thread.Sleep(3000);

        Text.Narrator("Links zie je een smalle doorgang naar een donkere, mysterieuze grot.");
        Thread.Sleep(3000);

        Text.Narrator("Rechts leidt een oud, kronkelig pad naar een verlaten dorp.");
        Thread.Sleep(3000);

        Console.Write("\nWat kies je? Typ 'links' of 'rechts': ");
        string choice = Console.ReadLine().ToLower();

        if (choice == "links" || choice == "l")
        {
            Console.Clear();
            Text.WriteGameName();
            Thread.Sleep(1000);
            
            Text.Narrator("Je ademt diep in en stapt de donkere grot binnen...");
            Thread.Sleep(2000);
            player.SetLastChoice("grot");
        }
        else if (choice == "rechts" || choice == "r")
        {
            Console.Clear();
            Text.WriteGameName();
            Thread.Sleep(1000);
            
            Text.Narrator("Je kiest het kronkelige pad en loopt richting het verlaten dorp...");
            Thread.Sleep(2000);

            Text.Narrator("De huizen zijn vervallen en de wind waait door de kapotte ramen.");
            Thread.Sleep(2000);

            Text.Narrator("Je hebt het gevoel dat je wordt bekeken... maar door wie of wat?");
            Thread.Sleep(2500);
            player.SetLastChoice("dorp");
        }
        else
        {
            Text.Narrator("Dat is geen geldige keuze.");
            Thread.Sleep(2000);
            question1();
        }
    }

    private void question2()
    {
        GameHelper.showLoading();
        Console.Clear();
        Text.WriteGameName();
        Thread.Sleep(1000);
        
        switch (player.GetLastChoice())
        {
            case "grot":
                Text.Narrator("In de grot zie je een vreemde fakkel aan de muur hangen.");
                Thread.Sleep(3000);
                
                Text.Narrator("Pak je de fakkel? (ja/nee)");
                string? choice = Console.ReadLine().ToLower();
                Console.Clear();
                
                if (choice == "ja" || choice == "j")
                {
                    Text.Narrator("Je pakt de fakkel en steekt hem aan.");
                    Thread.Sleep(2000);
                    player.AddToInventory("Fakkel", 1);
                    player.SetLastChoice("ja");
                }
                else if (choice == "nee" || choice == "n")
                {
                    Text.Narrator("Je besluit de fakkel te laten hangen.");
                    Thread.Sleep(2000);
                    player.SetLastChoice("nee");
                }
                else
                {
                    Text.Narrator("Dat is geen geldige keuze.");
                    Thread.Sleep(2000);
                    question2();
                }
                break;

            case "dorp":
                Text.Narrator("In het dorp sta je voor een oude herberg en een donkere steeg.");
                Thread.Sleep(3000);
                
                Text.Narrator("Wat kies je? Typ '1' voor de herberg of '2' voor de steeg.");
                choice = Console.ReadLine().ToLower();
                
                if (choice == "1")
                {
                    Text.Narrator("Je kiest voor de herberg en betreedt het donkere gebouw.");
                    Thread.Sleep(2000);
                    player.SetLastChoice("herberg");
                }
                else if (choice == "2")
                {
                    Text.Narrator("Je kiest de steeg en hoort vreemde geluiden achter je.");
                    Thread.Sleep(2000);
                    player.SetLastChoice("steeg");
                }
                else
                {
                    Text.Narrator("Dat is geen geldige keuze.");
                    Thread.Sleep(2000);
                    question2();
                }
                break;
        }
    }

    private void question3()
    {
        GameHelper.showLoading();
        Console.Clear();
        Text.WriteGameName();
        Thread.Sleep(1000);

        switch (player.GetLastChoice())
        {
            case "ja":
                Text.Narrator("Je komt vast te zitten in de grot en hoort iets groots bewegen in de duisternis.");
                Thread.Sleep(3000);
                
                Text.Narrator("Wat doe je? Klim je omhoog, verstop je je of gooi je de fakkel als afleiding?");
                Console.WriteLine("Typ 'klimmen', 'verstoppen' of 'gooien': ");
                string choice = Console.ReadLine().ToLower();
                Console.Clear();
                
                if (choice == "klimmen")
                {
                    Text.Narrator("Je klimt omhoog naar de opening en ontsnapt uit de grot.");
                    Thread.Sleep(2000);
                    player.SetLastChoice("klimmen");
                }
                else if (choice == "verstoppen")
                {
                    Text.Narrator("Je verstopt je, maar het wezen vindt je toch.");
                    Thread.Sleep(2000);
                    player.SetLastChoice("verstoppen");
                }
                else if (choice == "gooien")
                {
                    Text.Narrator("Je gooit de fakkel als afleiding en rent weg.");
                    Thread.Sleep(2000);
                    player.RemoveFromInventory("Fakkel", 1);
                    player.SetLastChoice("gooien");
                }
                Thread.Sleep(2000);
                break;
            case "nee":
                Text.Narrator("Je komt vast te zitten in de grot en hoort iets groots bewegen in de duisternis.");
                Thread.Sleep(3000);
                
                Text.Narrator("Wat doe je? Klim je omhoog of verstop je je?");
                choice = Console.ReadLine().ToLower();
                
                if (choice == "klimmen")
                {
                    Text.Narrator("Je klimt omhoog naar de opening en ontsnapt uit de grot.");
                    Thread.Sleep(2000);
                    player.SetLastChoice("klimmen");
                }
                else if (choice == "verstoppen")
                {
                    Text.Narrator("Je verstopt je, maar het wezen vindt je toch.");
                    Thread.Sleep(2000);
                    player.SetLastChoice("verstoppen");
                }
                break;

            case "dorp":
                break;
        }
    }
}