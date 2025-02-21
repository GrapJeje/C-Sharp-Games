using TBAG.Helpers;

namespace TBAG.Models.Questions;

public class Question2(Game game) : GameQuestion(2, game)
{
    private readonly Game _game = game;

    protected override void AskQuestion()
    {
        switch (_game.GetPlayer().GetLastChoice())
        {
            case "grot":
                Text.Narrator("In de grot zie je een vreemde fakkel aan de muur hangen.");
                Thread.Sleep(3000);

                Text.Narrator("Pak je de fakkel? (ja/nee)");
                var choice = Console.ReadLine()!.ToLower();
                Console.Clear();
                Text.WriteGameName();

                switch (choice)
                {
                    case "ja":
                    case "j":
                        Text.Narrator("Je pakt de fakkel en steekt hem aan.");
                        Thread.Sleep(2000);
                        _game.GetPlayer().AddToInventory("Fakkel", 1);
                        _game.GetPlayer().SetLastChoice("ja");
                        Thread.Sleep(1000);
                        break;
                    case "nee":
                    case "n":
                        Text.Narrator("Je besluit de fakkel te laten hangen.");
                        Thread.Sleep(2000);
                        _game.GetPlayer().SetLastChoice("nee");
                        break;
                    default:
                        NotPossible();
                        break;
                }
                break;

            case "dorp":
                Text.Narrator("In het dorp sta je voor een oude herberg en een donkere steeg.");
                Thread.Sleep(3000);

                Console.WriteLine("Wat kies je? Typ '1' voor de herberg of '2' voor de steeg.");
                choice = Console.ReadLine()!.ToLower();

                switch (choice)
                {
                    case "1":
                        Text.Narrator("Je kiest voor de herberg en betreedt het donkere gebouw.");
                        Thread.Sleep(2000);
                        _game.GetPlayer().SetLastChoice("herberg");
                        break;
                    case "2":
                        Text.Narrator("Je kiest de steeg en hoort vreemde geluiden achter je.");
                        Thread.Sleep(2000);
                        _game.GetPlayer().SetLastChoice("steeg");
                        break;
                    default:
                        NotPossible();
                        break;
                }
                break;
        }
    }
}