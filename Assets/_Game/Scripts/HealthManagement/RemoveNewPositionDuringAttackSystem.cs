using FFS.Libraries.StaticEcs;
using Game.Movement;

namespace Game.HealthManagement
{
  public class RemoveNewPositionDuringAttackSystem : IFrameSystem
  {
    public void Update()
    {
      foreach (var entity in GameWorld.QueryEntities.For<All<AttackAnimation, NewPosition>>()) {
        entity.Delete<NewPosition>();
      }
    }
  }
}
