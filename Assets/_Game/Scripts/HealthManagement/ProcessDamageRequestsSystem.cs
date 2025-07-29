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

      health.Value = Mathf.Max(health.Value - damageRequest.Amount, 0f);

      Log.Debug($"{target.ToString()} has {health.Value} of {health.MaxValue} health.");

      if (health.Value > 0f || !target.HasAllOfTags<Alive>()) {
        return;
      }

      Log.Debug($"{target.ToString()} dies.");

      target.DeleteTag<Alive>();
      target.SetTag<Dead>();
    }
  }
}
