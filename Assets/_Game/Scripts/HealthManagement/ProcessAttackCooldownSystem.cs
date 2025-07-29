using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace Game.HealthManagement
{
  public class ProcessAttackCooldownSystem : IFrameSystem
  {
    public void Update()
    {
      foreach (var entity in GameWorld.QueryEntities.For<All<AttackCooldown>>()) {
        ref var cooldown = ref entity.Ref<AttackCooldown>();

        cooldown.RemainingTime -= Time.deltaTime;

        if (cooldown.RemainingTime <= 0f) {
          entity.Delete<AttackCooldown>();
        }
      }
    }
  }
}
