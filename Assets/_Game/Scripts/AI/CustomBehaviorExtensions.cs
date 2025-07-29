using Omnihavior.Core;

namespace Game.AI
{
  public static class CustomBehaviorExtensions
  {
    public static Behavior ConvertToBehaviorComponent(this CustomBehavior<GameWorld.Entity> customBehavior)
    {
      return new() { Value = customBehavior, };
    }
  }
}
