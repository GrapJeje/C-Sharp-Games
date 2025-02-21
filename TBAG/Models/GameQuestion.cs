using TBAG.Helpers;

namespace TBAG.Models;

public abstract class GameQuestion(int id, Game game)
{
    public int Id { get; set; } = id;
    protected Game Game { get; set; } = game;

    public void ShowQuestion()
    {
        GameHelper.showLoading();
        Console.Clear();
        Text.WriteGameName();
        Thread.Sleep(1000);
        
        AskQuestion();
    }

    protected abstract void AskQuestion();

    protected void NotPossible()
    {
        Text.Narrator("Dat is geen geldige keuze.");
        Thread.Sleep(2000);
        AskQuestion();
    }
}