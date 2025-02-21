using TBAG.Helpers;

namespace TBAG.Models.Questions;

public class Question5(Game game) : GameQuestion(5, game)
{
    private readonly Game _game = game;

    protected override void AskQuestion()
    {
        Text.Narrator("Je komt bij een oude tempel. Boven de ingang staat: 'Alleen de waardigen mogen binnen.'");
        Thread.Sleep(3000);

        Text.Narrator("Er zijn twee deuren: één van goud en één van steen.");
        Thread.Sleep(3000);

        Console.WriteLine("Welke deur kies je? Typ 'goud' of 'steen': ");
        var choice = Console.ReadLine()!.ToLower();

        switch (choice)
        {
            case "goud":
                Text.Narrator("De gouden deur opent zich en je wordt begroet door een schitterend licht.");
                Thread.Sleep(3000);
                _game.GetPlayer().SetLastChoice("goud");
                break;
            case "steen":
                Text.Narrator("De stenen deur opent zich langzaam en je hoort een diepe stem: 'Welkom, avonturier.'");
                Thread.Sleep(3000);
                _game.GetPlayer().SetLastChoice("steen");
                break;
            default:
                NotPossible();
                break;
        }
    }
}