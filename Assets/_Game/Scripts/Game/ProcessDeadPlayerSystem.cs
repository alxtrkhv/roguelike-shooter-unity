using FFS.Libraries.StaticEcs;
using Game.App;
using Game.Characters;
using Game.HealthManagement;

namespace Game
{
  public class ProcessDeadPlayerSystem : IFrameSystem
  {
    public void Update()
    {
      foreach (var entity in GameWorld.QueryEntities.For<TagAll<Player, Dead>>()) {
        ProcessDeadPlayer(entity);
      }
    }

    private static void ProcessDeadPlayer(GameWorld.Entity entity)
    {
      entity.Disable();
      Log.Debug("Game Over.");
    }
  }
}
