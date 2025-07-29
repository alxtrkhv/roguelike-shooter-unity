using FFS.Libraries.StaticEcs;

namespace Game.HealthManagement
{
  public struct AttackRequest : IComponent
  {
    public GameWorld.Entity Source;
    public GameWorld.Entity Target;

    public static void Create(GameWorld.Entity source, GameWorld.Entity target)
    {
      GameWorld.Entity.New(new AttackRequest { Source = source, Target = target, });
    }
  }
}
