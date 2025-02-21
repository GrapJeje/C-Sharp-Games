using TBAG.Helpers;

namespace TBAG.Models.Questions;

public class Question3(Game game) : GameQuestion(3, game)
{
    private readonly Game _game = game;

    protected override void AskQuestion()
    {
        switch (_game.GetPlayer().GetLastChoice())
        {
            case "ja":
                Text.Narrator("Je komt vast te zitten in de grot en hoort iets groots bewegen in de duisternis.");
                Thread.Sleep(3000);
                
                Text.Narrator("Wat doe je? Klim je omhoog, verstop je je of gooi je de fakkel als afleiding?");
                Console.WriteLine("Typ 'klimmen', 'verstoppen' of 'gooien': ");
                var choice = Console.ReadLine()!.ToLower();
                Console.Clear();
                Text.WriteGameName();
                
                switch (choice)
                {
                    case "klimmen":
                        Text.Narrator("Je klimt omhoog naar de opening en ontsnapt uit de grot.");
                        Thread.Sleep(2000);
                        _game.GetPlayer().SetLastChoice("klimmen");
                        break;
                    case "verstoppen":
                        Text.Narrator("Je verstopt je, maar het wezen vindt je toch.");
                        Thread.Sleep(2000);
                        _game.GameOver(false, "Je bent opgegeten door een gruwelijk wezen.");
                        break;
                    case "gooien":
                        Text.Narrator("Je gooit de fakkel als afleiding en rent weg.");
                        Thread.Sleep(2000);
                        _game.GetPlayer().RemoveFromInventory("Fakkel", 1);
                        _game.GetPlayer().SetLastChoice("gooien");
                        break;
                    default:
                        NotPossible();
                        break;
                }
                Thread.Sleep(2000);
                break;
            case "nee":
                Text.Narrator("Je komt vast te zitten in de grot en hoort iets groots bewegen in de duisternis.");
                Thread.Sleep(3000);
                
                Text.Narrator("Wat doe je? Klim je omhoog of verstop je je?");
                choice = Console.ReadLine()!.ToLower();
                
                switch (choice)
                {
                    case "klimmen":
                        Text.Narrator("Je klimt omhoog naar de opening en ontsnapt uit de grot.");
                        Thread.Sleep(2000);
                        _game.GetPlayer().SetLastChoice("klimmen");
                        break;
                    case "verstoppen":
                        Text.Narrator("Je verstopt je, maar het wezen vindt je toch.");
                        Thread.Sleep(2000);
                        _game.GetPlayer().SetLastChoice("verstoppen");
                        break;
                    default:
                        NotPossible();
                        break;
                }
                break;

            case "herberg":
                Text.Narrator("In de herberg vind je een oude man die je een raadsel opgeeft.");
                Thread.Sleep(3000);
                
                Text.Narrator("Hij zegt: 'Ik spreek zonder mond en hoor zonder oren. Ik heb geen lichaam, maar ik kom tot leven met de wind. Wat ben ik?'");
                Thread.Sleep(4000);
                
                Console.WriteLine("Typ je antwoord (één woord): ");
                var answer = Console.ReadLine()!.ToLower();
                
                if (answer == "echo")
                {
                    Text.Narrator("De oude man knikt tevreden en geeft je een sleutel.");
                    Thread.Sleep(2000);
                    _game.GetPlayer().AddToInventory("Sleutel", 1);
                    _game.GetPlayer().SetLastChoice("steeg");
                    Text.Narrator("Je loop de herberg weer uit.");
                    Thread.Sleep(2000);
                    AskQuestion();
                }
                else
                {
                    Text.Narrator("De oude man schudt zijn hoofd en zegt: 'Probeer het later nog eens.'");
                    Thread.Sleep(2000);
                }
                break;

            case "steeg":
                Text.Narrator("In de steeg vind je een geheimzinnige doos met een slot.");
                Thread.Sleep(3000);
                
                if (_game.GetPlayer().HasItem("Sleutel"))
                {
                    Text.Narrator("Je gebruikt de sleutel om de doos te openen en vindt een oude kaart.");
                    Thread.Sleep(2000);
                    _game.GetPlayer().AddToInventory("Oude Kaart", 1);
                    _game.GetPlayer().SetLastChoice("kaart");
                }
                else
                {
                    Text.Narrator("Je hebt geen sleutel om de doos te openen.");
                    Thread.Sleep(2000);
                }
                break;
        }
    }
}