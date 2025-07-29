using FFS.Libraries.StaticEcs;
using Game.App;
using UnityEngine;

namespace Game.HealthManagement
{
  public class ProcessDamageRequestsSystem : IFrameSystem
  {
    public void Update()
    {
      foreach (var entity in GameWorld.QueryEntities.For<All<DamageRequest>>()) {
        ProcessRequest(entity);

        entity.Destroy();
      }
    }

    private static void ProcessRequest(World<GameWorld.Tag>.Entity entity)
    {
      ref var damageRequest = ref entity.Ref<DamageRequest>();

      var target = damageRequest.Target;
      if (!target.IsActual()) {
        return;
      }

      if (!target.HasAllOf<Health>()) {
        return;
      }

      ref var health = ref target.Ref<Health>();

      health.Value -= damageRequest.Amount;
      target.SetTag<HealthUpdated>();

      if (target.HasAllOf<HitView>()) {
        ref var hitView = ref target.Ref<HitView>();
        hitView.Value.Hit();
      }

      Log.Debug($"{target.ToString()} takes {damageRequest.Amount} damage.");
    }
  }
}
