using FFS.Libraries.StaticEcs;
using Game.Characters;
using Game.Movement;
using UnityEngine;

namespace Game.AI
{
  public class FindNearestEnemySystem : IFrameSystem
  {
    public void Update()
    {
      foreach (var entity in GameWorld.QueryEntities.For<TagAll<LookingForNearestEnemy>>()) {
        ref var currentPosition = ref entity.Ref<CurrentPosition>();

        GameWorld.Entity nearestEnemy = default;
        var nearestDistance = float.MaxValue;
        var foundEnemy = false;

        foreach (var enemyEntity in GameWorld.QueryEntities.For<TagAll<Enemy>>()) {
          ref var enemyPosition = ref enemyEntity.Ref<CurrentPosition>();

          var distance = Vector3.Distance(currentPosition.Value, enemyPosition.Value);

          if (distance >= nearestDistance) {
            continue;
          }

          nearestDistance = distance;
          nearestEnemy = enemyEntity;
          foundEnemy = true;
        }

        if (foundEnemy) {
          entity.Put(new NearestEnemy { Value = nearestEnemy });
        } else {
          entity.TryDelete<NearestEnemy>();
        }
      }
    }
  }
}
