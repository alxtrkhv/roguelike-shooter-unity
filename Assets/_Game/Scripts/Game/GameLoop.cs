using System;
using System.Collections.Generic;
using FFS.Libraries.StaticEcs;
using VContainer.Unity;

namespace Game
{
  public class GameLoop : IInitializable, IDisposable, ITickable, IFixedTickable
  {
    private readonly IReadOnlyList<IFrameSystem> _frameSystems;
    private readonly IReadOnlyList<ITickSystem> _tickSystems;
    private readonly IReadOnlyList<ICallOnceSystem> _callOnceSystems;

    public GameLoop(IReadOnlyList<IFrameSystem> frameSystems, IReadOnlyList<ITickSystem> tickSystems,
      IReadOnlyList<ICallOnceSystem> callOnceSystems)
    {
      _frameSystems = frameSystems;
      _tickSystems = tickSystems;
      _callOnceSystems = callOnceSystems;
    }

    public bool IsPlaying { get; private set; }

    public void Initialize()
    {
      var worldConfig = WorldConfig.Default();

      GameWorld.Create(worldConfig);
      GameWorld.RegisterComponents();
      GameWorld.Initialize();

      FrameSystems.Create();
      TickSystems.Create();

      foreach (var callOnceSystem in _callOnceSystems) {
        FrameSystems.AddCallOnce(callOnceSystem);
      }

      foreach (var frameSystem in _frameSystems) {
        FrameSystems.AddUpdate(frameSystem);
      }

      foreach (var tickSystem in _tickSystems) {
        TickSystems.AddUpdate(tickSystem);
      }

      FrameSystems.Initialize();
      TickSystems.Initialize();
    }

    public void Play()
    {
      if (IsPlaying) {
        return;
      }

      IsPlaying = true;
    }

    public void Pause()
    {
      if (!IsPlaying) {
        return;
      }

      IsPlaying = false;
    }

    public void Tick()
    {
      if (!IsPlaying) {
        return;
      }

      FrameSystems.Update();
    }

    public void FixedTick()
    {
      if (!IsPlaying) {
        return;
      }

      TickSystems.Update();
    }

    public void Dispose()
    {
      IsPlaying = false;

      TickSystems.Destroy();
      FrameSystems.Destroy();
      GameWorld.Destroy();
    }
  }
}
