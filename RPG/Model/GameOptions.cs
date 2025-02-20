using System.Runtime.CompilerServices;

namespace RGP.Model;

public class GameOptions
{
    private static PlayableCharacter playerCharacter { get; set; }
    private static PlayableCharacter enemyCharacter { get; set; }
    
    public GameOptions(PlayableCharacter playerCharacter, PlayableCharacter enemyCharacter)
    {
        GameOptions.playerCharacter = playerCharacter;
        GameOptions.enemyCharacter = enemyCharacter;
    }
    
    public Player GetPlayer(Game game)
    {
        return new Player(game);
    }
    
    public Enemy GetEnemy(Game game)
    {
        return new Enemy(game);
    }
    
    public class Player
    {
        private Game game { get; set; }

        public Player(Game game)
        {
            this.game = game;
        }

        public void attack()
        {
            if (playerCharacter.Mana < 10)
            {
                Console.WriteLine("Je hebt niet genoeg mana om aan te vallen!");
                game.getChooseMenu();
                return;
            }

            Console.Clear();
            playerCharacter.Mana -= 10;

            if (enemyCharacter.Block)
            {
                Console.WriteLine("De vijand heeft geblokt");
                enemyCharacter.Block = false;
                return;
            }

            long random = new Random().NextInt64(1, 10);
            int attackDamage;
            bool isCritical = false;
            
            if (random == 1)
            {
                attackDamage = playerCharacter.Character.AttackDamage * 2;
                isCritical = true;
            }
            else
            {
                attackDamage = playerCharacter.Character.AttackDamage;
            }
            
            if (isCritical)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Critical hit!");
                Console.ResetColor();
            }

            enemyCharacter.Health -= attackDamage;
            if (enemyCharacter.Health <= 0)
            {
                game.win();
                return;
            }

            Console.Write("Jij hebt " + enemyCharacter.Character.Name + " aangevallen voor ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(attackDamage);
            Console.ResetColor();
            Console.WriteLine(" schade!");


            Console.Write(enemyCharacter.Character.Name + " heeft nog ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(enemyCharacter.Health);
            Console.ResetColor();
            Console.Write(" hp over!");
            Thread.Sleep(2000);
        }

        public void block()
        {
            Console.Clear();
            if (playerCharacter.Mana < 25)
            {
                Console.WriteLine("Je hebt niet genoeg mana om aan te blocken!");
                game.getChooseMenu();
                return;
            }

            playerCharacter.Mana -= 25;
            playerCharacter.Block = true;
            Console.WriteLine("Je hebt geblokt!");
            Thread.Sleep(2000);
        }

        public void heal()
        {
            Console.Clear();
            if (playerCharacter.Mana < 30)
            {
                Console.WriteLine("Je hebt niet genoeg mana om te healen!");
                game.getChooseMenu();
                return;
            }

            playerCharacter.Mana -= 30;
            playerCharacter.Health += 50;
            Console.WriteLine("Je hebt jezelf geheald voor 50 hp!");
            Thread.Sleep(2000);
        }

        public void skip()
        {
            Console.Clear();
            Console.WriteLine("Je hebt je beurt overgeslagen!");
            Console.WriteLine("Jij hebt je beurt overgeslagen, hierdoor krijg je 15 mana derbij!");
            playerCharacter.Mana += 15;
            Thread.Sleep(2000);
        }
    }

    public class Enemy
    {
        private Game game { get; set; }
        
        public Enemy(Game game)
        {
            this.game = game;
        }

        public void getRandomOption()
        {
            Random random = new Random();
            List<Action> availableActions = new List<Action>();

            if (enemyCharacter.Health <= enemyCharacter.Character.Health)
            {
                if (enemyCharacter.Mana >= 30)
                    availableActions.Add(() => UseSkill(30));

                if (enemyCharacter.Mana >= 25)
                    availableActions.Add(() => UseSkill(25));

                if (enemyCharacter.Mana >= 10)
                    availableActions.Add(() => UseSkill(10));
            }
            else
            {
                if (enemyCharacter.Mana >= 10)
                    availableActions.Add(() => UseSkill(10));

                if (enemyCharacter.Mana >= 25)
                    availableActions.Add(() => UseSkill(25));
            }

            if (availableActions.Count > 0)
            {
                int index = random.Next(availableActions.Count);
                availableActions[index]();
            }
            else
            {
                skip();
            }
        }
        
        private void UseSkill(int manaCost)
        {
            switch (manaCost)
            {
                case 10:
                    attack();
                    break;
                case 25:
                    block();
                    break;
                case 30:
                    heal();
                    break;
            }
        }

        public void attack()
        {
            Console.Clear();
            enemyCharacter.Mana -= 10;
            
            if (playerCharacter.Block)
            {
                Console.WriteLine("De vijand probeerde aantevallen, maar jij hebt het geblokkeerd.");
                playerCharacter.Block = false;
                return;
            }
            
            long random = new Random().NextInt64(1, 10);
            int attackDamage;
            bool isCritical = false;
            
            if (random == 1)
            {
                attackDamage = playerCharacter.Character.AttackDamage * 2;
                isCritical = true;
            }
            else
            {
                attackDamage = playerCharacter.Character.AttackDamage;
            }
            
            if (isCritical)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Critical hit!");
                Console.ResetColor();
            }

            playerCharacter.Health -= attackDamage;
            if (playerCharacter.Health <= 0)
            {
                game.lose();
                return;
            }

            Console.Write("De vijand heeft jouw aangevallen voor ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(attackDamage);
            Console.ResetColor();
            Console.WriteLine(" schade!");


            Console.Write("Jij hebt nog ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(playerCharacter.Health);
            Console.ResetColor();
            Console.Write(" hp over!");
            Thread.Sleep(2000);
        }
        
        public void block()
        {
            Console.Clear();

            enemyCharacter.Mana -= 25;
            enemyCharacter.Block = true;
            Console.WriteLine("De vijand heeft geblokt!");
            Thread.Sleep(2000);
        }
        
        public void heal()
        {
            Console.Clear();

            enemyCharacter.Mana -= 30;
            enemyCharacter.Health += 50;
            Console.WriteLine("De vijand heeft zichzelf voor 50 hp geheald!");
            Thread.Sleep(2000);
        }
        
        public void skip()
        {
            Console.Clear();
            Console.WriteLine("De vijand heeft zijn beurt overgeslagen, en krijgt 15 mana derbij!");
            enemyCharacter.Mana += 15;
            Thread.Sleep(2000);
        }
    }
}