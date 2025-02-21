using TBAG.Helpers;

namespace TBAG.Models.Questions;

public class Question1(Game game) : GameQuestion(6, game)
{
    private readonly Game _game = game;

    protected override void AskQuestion()
    {
        Text.Narrator("Na een lange tocht door het dichte woud kom je bij een splitsing...");
        Thread.Sleep(3000);

        Text.Narrator("Links zie je een smalle doorgang naar een donkere, mysterieuze grot.");
        Thread.Sleep(3000);

        Text.Narrator("Rechts leidt een oud, kronkelig pad naar een verlaten dorp.");
        Thread.Sleep(3000);

        Console.Write("\nWat kies je? Typ 'links' of 'rechts': ");
        var choice = Console.ReadLine()!.ToLower();

        switch (choice)
        {
            case "links":
            case "l":
                Console.Clear();
                Text.WriteGameName();
                Thread.Sleep(1000);
            
                Text.Narrator("Je ademt diep in en stapt de donkere grot binnen...");
                Thread.Sleep(2000);
                _game.GetPlayer().SetLastChoice("grot");
                break;
            case "rechts":
            case "r":
                Console.Clear();
                Text.WriteGameName();
                Thread.Sleep(1000);
            
                Text.Narrator("Je kiest het kronkelige pad en loopt richting het verlaten dorp...");
                Thread.Sleep(2000);

                Text.Narrator("De huizen zijn vervallen en de wind waait door de kapotte ramen.");
                Thread.Sleep(2000);

                Text.Narrator("Je hebt het gevoel dat je wordt bekeken... maar door wie of wat?");
                Thread.Sleep(2500);
                _game.GetPlayer().SetLastChoice("dorp");
                break;
            default:
                NotPossible();
                break;
        }
    }
}