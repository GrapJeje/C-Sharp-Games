using TBAG.Helpers;

namespace TBAG.Models.Questions;

public class Question6(Game game) : GameQuestion(6, game)
{
    private readonly Game _game = game;

    protected override void AskQuestion()
    {
        Text.Narrator("Je staat nu voor de schatkamer van Eldoria.");
        Thread.Sleep(3000);

        if (_game.GetPlayer().HasItem("Oude Kaart"))
        {
            Text.Narrator("Met de oude kaart vind je de verborgen schat zonder problemen.");
            Thread.Sleep(3000);
            _game.GameOver(true, null);
        }
        else
        {
            Text.Narrator("Zonder de kaart kun je de schat niet vinden. Je bent verdwaald...");
            Thread.Sleep(2000);
            _game.GameOver(false, "Je bent verdwaald en komt nooit meer terug.");
        }
    }
}