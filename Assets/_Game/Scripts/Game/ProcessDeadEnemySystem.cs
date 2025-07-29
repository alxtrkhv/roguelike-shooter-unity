using FFS.Libraries.StaticEcs;
using Game.App;
using Game.Characters;
using Game.HealthManagement;

namespace Game
{
  public class ProcessDeadEnemySystem : IFrameSystem
  {
    public void Update()
    {
      foreach (var entity in GameWorld.QueryEntities.For<TagAll<Enemy, Dead>>()) {
        ProcessDeadEnemy(entity);
      }
    }

    private static void ProcessDeadEnemy(GameWorld.Entity entity)
    {
      entity.Disable();
      Log.Debug("Enemy defeated.");
    }
  }
}
