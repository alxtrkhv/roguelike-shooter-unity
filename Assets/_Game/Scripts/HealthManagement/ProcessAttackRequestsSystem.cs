using FFS.Libraries.StaticEcs;
using Game.App;
using Game.Items;
using Game.Movement;

namespace Game.HealthManagement
{
  public class ProcessAttackRequestsSystem : IFrameSystem
  {
    public void Update()
    {
      foreach (var entity in GameWorld.QueryEntities.For<All<AttackRequest>>()) {
        ProcessRequest(entity);

        entity.Destroy();
      }
    }

    private static void ProcessRequest(World<GameWorld.Tag>.Entity entity)
    {
      ref var attackRequest = ref entity.Ref<AttackRequest>();

      var source = attackRequest.Source;
      var target = attackRequest.Target;

      if (source.HasAllOf<AttackCooldown>()) {
        return;
      }

      if (source.HasAllOf<NewPosition>()) {
        return;
      }

      if (!target.HasAllOfTags<Alive>()) {
        return;
      }

      ref var equipment = ref source.Ref<Equipment>();
      var weapon = equipment.Weapon;

      source.Put(new AttackCooldown(weapon.Cooldown));
      source.Put(new AttackAnimation(0.5f));

      DamageRequest.Create(source, target, weapon.Damage);

      Log.Debug($"{source.ToString()} tries to attack {target.ToString()} inflicting {weapon.Damage} damage.");
    }
  }
}
