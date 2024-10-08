Console.WriteLine("\nRegels om te winnen van STEEN-PAPIER-SCHAAR zijn: \n" 
+ "Steen vs Papier -> Papier wint\n"
+ "Steen vs Schaar -> Steen wint\n"
+ "Papier vs Schaar -> Schaar wint\n");

while (true)
{
    System.Threading.Thread.Sleep(1000);
    Console.WriteLine("Vul jouw keuze in \n"
    + "1 - steen\n"
    + "2 - papier\n"
    + "3 - schaar\n");

    int player_choice;
    Console.Write("Vul jouw keuze in: ");

    if (!int.TryParse(Console.ReadLine(), out player_choice))
    {
        Console.WriteLine("Ongeldige invoer, voer een getal in.\n");
        continue;
    }

    if (!(player_choice == 1 || player_choice == 2 || player_choice == 3))
    {
        Console.WriteLine("Ongeldige keuze, kies uit 1, 2 of 3.\n");
        continue;
    }

    string player_word_choice = ($"{(player_choice == 1 ? "Steen" : player_choice == 2 ? "Papier" : "Schaar")}");
    
    Console.WriteLine("Jouw keuze is: " + player_word_choice);
    System.Threading.Thread.Sleep(1000);

    Console.WriteLine("Nu is het de bot zijn keus...");
    System.Threading.Thread.Sleep(1000);

    Random random = new Random();
    int computer_choice = random.Next(1, 4);
    string computer_word_choice = ($"{(computer_choice == 1 ? "Steen" : computer_choice == 2 ? "Papier" : "Schaar")}");

    Console.WriteLine("De bot zijn keuse is: " + computer_word_choice);
    System.Threading.Thread.Sleep(1000);

    Console.WriteLine(player_word_choice + " vs " + computer_word_choice);
    System.Threading.Thread.Sleep(1000);

    if (player_choice == computer_choice) 
    {
        Console.WriteLine("<== Het is gelijkspel! ==>");
    }
    else if (player_choice == 1 && computer_choice == 3 
    || player_choice == 2 && computer_choice == 1 
    || player_choice == 3 && computer_choice == 2) 
    {
        Console.WriteLine("<== Jij hebt gewonnen! ==>");
    }
    else 
    {
        Console.WriteLine("<== De bot heeft gewonnen! ==>");
    }

    System.Threading.Thread.Sleep(1000);
    Console.WriteLine("Wil je nog een keer spelen? (J/N)");
    string ans = Console.ReadLine().ToLower();

    if (ans == "n" || ans == "nee") 
    {
        break;
    }
    Console.WriteLine("");
}

Console.WriteLine("Bedankt voor het spelen!");