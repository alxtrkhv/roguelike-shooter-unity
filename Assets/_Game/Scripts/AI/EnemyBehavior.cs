using FFS.Libraries.StaticEcs;
using Game.Characters;
using Game.HealthManagement;
using Game.Items;
using Game.Movement;
using Omnihavior.Core;

namespace Game.AI
{
  public class EnemyBehavior : CustomBehavior<GameWorld.Entity>
  {
    public EnemyBehavior()
    {
      var checkSelfAlive = Builder.Lambda(entity => {
          if (!entity.HasAllOfTags<Alive>()) {
            return NodeStatus.Failure;
          }

          return NodeStatus.Success;
        }
      );

      var checkPlayerAlive = Builder.Lambda(entity => {
          var playerAlive = GameWorld.QueryEntities.For<TagAll<Player>>().First(out var playerEntity);
          if (playerAlive) {
            entity.Put(new Target { Value = playerEntity });
            return NodeStatus.Success;
          }

          entity.TryDelete<Target>();
          entity.TryDelete<TargetDirection>();
          return NodeStatus.Failure;
        }
      );

      var canAttack = Builder.Lambda(entity => {
          ref var target = ref entity.Ref<Target>();
          var targetEntity = target.Value;

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

      var followPlayer = Builder.Lambda(entity => {
          ref var target = ref entity.Ref<Target>();
          var targetEntity = target.Value;

          ref var targetPosition = ref targetEntity.Ref<CurrentPosition>();
          ref var currentPosition = ref entity.Ref<CurrentPosition>();

          var direction = (targetPosition.Value - currentPosition.Value).normalized;

          entity.Put(new TargetDirection { Value = direction, });

          return NodeStatus.Success;
        }
      );

      var stop = Builder.Lambda(entity => {
          entity.TryDelete<TargetDirection>();
          return NodeStatus.Success;
        }
      );

      var attack = Builder.Lambda(entity => {
          ref var target = ref entity.Ref<Target>();
          var targetEntity = target.Value;

          AttackRequest.Create(entity, targetEntity);

          return NodeStatus.Success;
        }
      );

      Root = Builder.Sequence(
        checkSelfAlive,
        checkPlayerAlive,
        canAttack.AsCondition(stop.And(attack), followPlayer)
      ).Or(stop);
    }
  }
}
