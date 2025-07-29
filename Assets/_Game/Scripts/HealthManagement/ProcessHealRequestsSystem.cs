using FFS.Libraries.StaticEcs;
using Game.App;
using UnityEngine;

namespace Game.HealthManagement
{
  public class ProcessHealRequestsSystem : IFrameSystem
  {
    public void Update()
    {
      foreach (var entity in GameWorld.QueryEntities.For<All<HealRequest>>()) {
        ProcessRequest(entity);

        entity.Destroy();
      }
    }

    private static void ProcessRequest(World<GameWorld.Tag>.Entity entity)
    {
      ref var healRequest = ref entity.Ref<HealRequest>();

      var target = healRequest.Target;
      if (!target.IsActual()) {
        return;
      }

      if (!target.HasAllOf<Health>()) {
        return;
      }

      ref var health = ref target.Ref<Health>();

      health.Value += healRequest.Amount;
      target.SetTag<HealthUpdated>();

      Log.Debug($"{target.ToString()} receives {healRequest.Amount} heal.");
    }
  }
}
