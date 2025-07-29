using FFS.Libraries.StaticEcs;
using Game.Characters;
using Game.HealthManagement;
using Game.Movement;
using UnityEngine;

namespace Game
{
  public abstract class Authoring : MonoBehaviour
  {
    public EntityGID Entity { get; private set; }

    public void BakeEntity()
    {
      if (Entity.TryUnpack<GameWorld.Tag>(out _)) {
        return;
      }

      var entity = OnBake();
      Entity = entity.Gid();

      entity.Add(
        new TransformLink { Value = transform, },
        new CurrentPosition { Value = transform.localPosition, },
        new AuthoringLink { Value = this, }
      );
    }

    protected virtual GameWorld.Entity OnBake() => GameWorld.Entity.New();

    public void DisposeEntity()
    {
      if (!Entity.TryUnpack<GameWorld.Tag>(out var entity)) {
        return;
      }

      OnDispose();

      if (!Entity.TryUnpack(out entity)) {
        return;
      }

      entity.Destroy();
    }

    protected virtual void OnDispose() { }
  }
}
