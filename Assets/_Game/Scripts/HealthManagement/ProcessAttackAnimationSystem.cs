using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace Game.HealthManagement
{
  public class ProcessAttackAnimationSystem : IFrameSystem
  {
    public void Update()
    {
      foreach (var entity in GameWorld.QueryEntities.For<All<AttackAnimation>>()) {
        ref var animation = ref entity.Ref<AttackAnimation>();

        animation.RemainingTime -= Time.deltaTime;

        if (animation.RemainingTime <= 0f) {
          entity.Delete<AttackAnimation>();
        }
      }
    }
  }
}
