using FFS.Libraries.StaticEcs;
using Game.App;
using UnityEngine;

namespace Game.HealthManagement
{
  public class UpdateHealthSystem : IFrameSystem
  {
    public void Update()
    {
      foreach (var entity in GameWorld.QueryEntities.For<All<Health>, TagAll<HealthUpdated>>()) {
        ProcessHealthUpdate(entity);
        entity.DeleteTag<HealthUpdated>();
      }
    }

    private static void ProcessHealthUpdate(World<GameWorld.Tag>.Entity entity)
    {
      ref var health = ref entity.Ref<Health>();

      health.Value = Mathf.Clamp(health.Value, 0f, health.MaxValue);

      Log.Debug($"{entity.ToString()} has {health.Value} of {health.MaxValue} health.");

      if (entity.HasAllOf<HealthView>()) {
        ref var healthView = ref entity.Ref<HealthView>();
        healthView.Value.SetHealth(health.Value, health.MaxValue);
      }

      switch (health.Value) {
        case <= 0f when entity.HasAllOfTags<Alive>():
          Log.Debug($"{entity.ToString()} dies.");
          entity.DeleteTag<Alive>();
          entity.SetTag<Dead>();

          if (entity.HasAllOf<HealthView>()) {
            ref var healthView = ref entity.Ref<HealthView>();
            healthView.Value.SetAlive(false);
          }

          break;

        case > 0f when entity.HasAllOfTags<Dead>():
          Log.Debug($"{entity.ToString()} is revived.");
          entity.DeleteTag<Dead>();
          entity.SetTag<Alive>();

          if (entity.HasAllOf<HealthView>()) {
            ref var healthView = ref entity.Ref<HealthView>();
            healthView.Value.SetAlive(true);
          }

          break;
      }
    }
  }
}
