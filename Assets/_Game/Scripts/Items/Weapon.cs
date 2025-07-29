namespace Game.Items
{
  public enum WeaponType
  {
    Melee,
    Ranged,
  }

  public struct Weapon
  {
    public float Damage;
    public WeaponType Type;
    public string Name;
    public float Cooldown;

    public static readonly Weapon Sword = new() {
      Damage = 10f,
      Type = WeaponType.Melee,
      Name = "Sword",
      Cooldown = 1f,
    };

    public static readonly Weapon Bow = new() {
      Damage = 8f,
      Type = WeaponType.Ranged,
      Name = "Bow",
      Cooldown = 1.5f,
    };

    public float Range()
    {
      return Type switch {
        WeaponType.Melee => 1.5f,
        WeaponType.Ranged => float.MaxValue,
        _ => -1f,
      };
    }

    public readonly Equipment ConvertToEquipment()
    {
      return new() {
        Weapon = this,
      };
    }
  }
}
