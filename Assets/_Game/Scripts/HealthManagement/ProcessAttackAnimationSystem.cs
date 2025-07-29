using FFS.Libraries.StaticEcs;
using Game.Items;
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

        if (entity.HasAllOf<WeaponView>()) {
          ref var weaponView = ref entity.Ref<WeaponView>();
          var progress = 1f - (animation.RemainingTime / animation.Duration);
          weaponView.Value.PlayWeaponAnimation(progress);
        }

        if (animation.RemainingTime <= 0f) {
          entity.Delete<AttackAnimation>();
        }
      }
    }
  }
}
