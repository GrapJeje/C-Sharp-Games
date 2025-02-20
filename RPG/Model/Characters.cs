namespace RGP.Model;

public class Character
{
    public int Id { get; }
    public string Name { get; }
    public int Health { get; }
    public int AttackDamage { get; }
    public int Mana { get; }

    public Character(int id, string name, int health, int attackDamage, int mana = 100)
    {
        Id = id;
        Name = name;
        Health = health;
        AttackDamage = attackDamage;
        Mana = mana;
    }
    
    public override string ToString() => $"{Id} - {Name} (Health: {Health}, Attack Damage: {AttackDamage}, Mana: {Mana})";
}

public static class Characters
{
    public static readonly Dictionary<string, Character> AllCharacters = new()
    {
        { "DRAVEN", new Character(1, "Draven", 250, 70) },
        { "LYRA", new Character(2, "Lyra", 180, 90) },
        { "GORVAK", new Character(3, "Gorvak", 400, 50) },
        { "ZYPHER", new Character(4, "Zypher", 200, 85) },
        { "THALGRIM", new Character(5, "Thalgrim", 220, 75) },
        { "ERAPHINA", new Character(6, "Eraphina", 300, 60) },
        { "RAGNAR", new Character(7, "Ragnar", 270, 65) },
        { "IGNIS", new Character(8, "Ignis", 230, 80) }
    };
    
    public static Character? GetById(int id)
    {
        return AllCharacters.Values.FirstOrDefault(c => c.Id == id);
    }
}

public class PlayableCharacter
{
    public Character Character;
    public int Health;
    public int Mana;
    public bool Block;
    
    public PlayableCharacter(int id)
    {
        Character = Characters.GetById(id);
        Health = Character.Health;
        Mana = Character.Mana;
        Block = false;
    }
}