using TBAG.Helpers;
using TBAG.Models.Questions;

namespace TBAG.Models;

public class Game
{
    private bool _isRunning = true;
    private readonly int _currentQuestionId = 1;
    private Player _player = null!;

    public Game()
    {
        Register();
        WriteStartStory();
        
        Dictionary<int, GameQuestion> questions = new()
        {
            { 1, new Question1(this) },
            { 2, new Question2(this) },
            { 3, new Question3(this) },
            { 4, new Question4(this) },
            { 5, new Question5(this) },
            { 6, new Question6(this) }
        };
        
        while (_isRunning)
        {
            if (questions.ContainsKey(_currentQuestionId))
            {
                questions[_currentQuestionId].ShowQuestion();
                _currentQuestionId++;
            }
            else
            {
                _isRunning = false;
                GameOver(false, "Er is iets fout gegaan.");
            }
        }
    }

    private void Register()
    {
        string? username = null;
        while (username == null || username.Length < 3)
        {
            Console.Clear();
            Text.WriteGameName();
            
            if (username is { Length: < 3 })
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
        _player = new Player(username);
    }

    private void WriteStartStory()
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
        
        _player.AddToInventory("Backpack", 1);
        Thread.Sleep(1500);
    }
    
    public Player GetPlayer()
    {
        return _player;
    }

    public void GameOver(bool won, string? deathMessage)
    {
        Console.Clear();
        Text.WriteGameName();
        Thread.Sleep(1000);

        if (won)
        {
            Text.Narrator("Gefeliciteerd! Je hebt het avontuur overleefd.");
            Thread.Sleep(2000);
            Text.Narrator("Je hebt de schat govonden en bent nu rijk!");
        }
        else
        {
            Text.Narrator("GAME OVER");
            Thread.Sleep(1500);
            Text.Narrator("Jouw reis eindigt hier...");
            
            if (!string.IsNullOrEmpty(deathMessage))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(deathMessage);
                Console.ResetColor();
            }
            
            Thread.Sleep(2000);
            Text.Narrator("De duisternis sluit zich om je heen...");
        }
        
        Thread.Sleep(2000);
        Text.Narrator("Bedankt voor het spelen!");
        _isRunning = false;
    }
}