using TBAG.Helpers;

namespace TBAG.Models.Questions;

public class Question4(Game game) : GameQuestion(4, game)
{
    private readonly Game _game = game;

    protected override void AskQuestion()
    {
        Text.Narrator("Je komt bij een oude brug die over een diepe kloof loopt.");
        Thread.Sleep(3000);

        Text.Narrator("De brug ziet er wankel uit, maar je moet naar de andere kant.");
        Thread.Sleep(3000);

        Text.Narrator("Je ziet ook een pad dat langs de kloof loopt, maar dat lijkt veel langer.");
        Thread.Sleep(3000);

        Console.WriteLine("Wat kies je? Typ 'brug' of 'pad': ");
        var choice = Console.ReadLine()!.ToLower();

        switch (choice)
        {
            case "brug":
            {
                Text.Narrator("Je besluit de brug over te steken...");
                Thread.Sleep(2000);

                if (_game.GetPlayer().HasItem("Fakkel"))
                {
                    Text.Narrator("Met de fakkel zie je dat de brug inderdaad wankel is, maar je kunt veilig oversteken.");
                    Thread.Sleep(3000);
                    _game.GetPlayer().SetLastChoice("brug");
                }
                else
                {
                    Text.Narrator("Zonder licht struikel je en val je van de brug...");
                    Game.GameOver(false, "Je bent te pletter gevallen.");
                }

                break;
            }
            case "pad":
                Text.Narrator("Je kiest het langere pad langs de kloof.");
                Thread.Sleep(2000);
                _game.GetPlayer().SetLastChoice("pad");
                break;
            default:
                NotPossible();
                break;
        }
    }
}