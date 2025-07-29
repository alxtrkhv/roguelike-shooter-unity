using FFS.Libraries.StaticEcs;
using Game.Characters;
using Game.HealthManagement;
using Game.Items;
using Game.Movement;
using Omnihavior.Core;

namespace Game.AI
{
  public class PlayerBehavior : CustomBehavior<GameWorld.Entity>
  {
    public PlayerBehavior()
    {
      var checkSelfAlive = Builder.Lambda(entity => {
          if (entity.HasAllOfTags<Alive>()) {
            return NodeStatus.Success;
          }

          return NodeStatus.Failure;
        }
      );

      var hasNearestEnemy = Builder.Lambda(entity => {
          if (!entity.HasAllOf<NearestEnemy>()) {
            return NodeStatus.Failure;
          }

          ref var nearestEnemy = ref entity.Ref<NearestEnemy>();
          var targetEntity = nearestEnemy.Value;

          if (targetEntity.HasAllOfTags<Alive>()) {
            return NodeStatus.Success;
          }

          return NodeStatus.Failure;
        }
      );

      var canAttack = Builder.Lambda(entity => {
          ref var nearestEnemy = ref entity.Ref<NearestEnemy>();
          var targetEntity = nearestEnemy.Value;

          ref var targetPosition = ref targetEntity.Ref<CurrentPosition>();
          ref var currentPosition = ref entity.Ref<CurrentPosition>();

          ref var equipment = ref entity.Ref<Equipment>();
          var range = equipment.Weapon.Range();

          var distance = (targetPosition.Value - currentPosition.Value).magnitude;
          if (distance > range) {
            return NodeStatus.Failure;
          }

          return NodeStatus.Success;
        }
      );

      var attack = Builder.Lambda(entity => {
          ref var nearestEnemy = ref entity.Ref<NearestEnemy>();
          var targetEntity = nearestEnemy.Value;

          AttackRequest.Create(entity, targetEntity);

          return NodeStatus.Success;
        }
      );

      Root = Builder.Sequence(
        checkSelfAlive,
        hasNearestEnemy,
        canAttack.AsCondition(attack)
      );
    }
  }
}
