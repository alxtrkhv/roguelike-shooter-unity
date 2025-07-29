using FFS.Libraries.StaticEcs;
using UnityEngine;

namespace Game.Movement
{
  public class RemoveRedundantNewPositionSystem : IFrameSystem
  {
    public void Update()
    {
      foreach (var entity in GameWorld.QueryEntities.For<All<CurrentPosition, NewPosition>>()) {
        ref var currentPosition = ref entity.Ref<CurrentPosition>();
        ref var newPosition = ref entity.Ref<NewPosition>();

        if (currentPosition.Value == newPosition.Value) {
          entity.Delete<NewPosition>();
        }
      }
    }
  }
}
