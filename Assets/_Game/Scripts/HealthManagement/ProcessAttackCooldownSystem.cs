using FFS.Libraries.StaticEcs;
using Game.Items;
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

        if (entity.HasAllOf<WeaponView>()) {
          ref var weaponView = ref entity.Ref<WeaponView>();
          var progress = 1f - (cooldown.RemainingTime / cooldown.Duration);
          weaponView.Value.SetAttackCooldown(progress);
        }

        if (cooldown.RemainingTime <= 0f) {
          entity.Delete<AttackCooldown>();
        }
      }
    }
  }
}
