using FFS.Libraries.StaticEcs;

namespace Game.HealthManagement
{
  public struct DamageRequest : IComponent
  {
    public float Amount;
    public GameWorld.Entity Source;
    public GameWorld.Entity Target;

    public static void Create(GameWorld.Entity source, GameWorld.Entity target, float amount)
    {
      GameWorld.Entity.New(new DamageRequest { Source = source, Target = target, Amount = amount, });
    }
  }
}
