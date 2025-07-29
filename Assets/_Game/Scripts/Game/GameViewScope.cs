using System;
using FFS.Libraries.StaticEcs;
using Game.AI;
using Game.HealthManagement;
using Game.Input;
using Game.Movement;
using VContainer;
using VContainer.Unity;

namespace Game
{
  public class GameViewScope : LifetimeScope
  {
    protected override void Configure(IContainerBuilder builder)
    {
      builder.RegisterInstance(GetComponent<GameView>());
      builder.Register<GameLoop>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();

      // Init
      RegisterSystem<BakeAuthoringsSystem>();

      // Frame
      RegisterSystem<FindNearestEnemySystem>();

      RegisterSystem<RunBehaviorsSystem>();
      RegisterSystem<ProcessPlayerInputSystem>();

      RegisterSystem<ProcessTargetPositionSystem>();
      RegisterSystem<ProcessTargetDirectionSystem>();
      RegisterSystem<RemoveRedundantNewPositionSystem>();

      RegisterSystem<ProcessAttackRequestsSystem>();
      RegisterSystem<ProcessHealRequestsSystem>();
      RegisterSystem<ProcessDamageRequestsSystem>();
      RegisterSystem<UpdateHealthSystem>();
      RegisterSystem<ProcessAttackCooldownSystem>();
      RegisterSystem<ProcessAttackAnimationSystem>();
      RegisterSystem<RemoveNewPositionDuringAttackSystem>();

      RegisterSystem<ApplyNewPositionSystem>();

      RegisterSystem<ProcessDeadPlayerSystem>();
      RegisterSystem<ProcessDeadEnemySystem>();

      // Destroy
      RegisterSystem<DestroyAuthoringSystem>();

      RegistrationBuilder RegisterSystem<TSystem>() where TSystem : ISystem
      {
        var systemType = typeof(TSystem);
        RegistrationBuilder? registration = null;

        if (typeof(IFrameSystem).IsAssignableFrom(systemType)) {
          registration = builder.Register<TSystem>(Lifetime.Scoped).As<IFrameSystem>();
        }

        if (typeof(ITickSystem).IsAssignableFrom(systemType)) {
          registration = builder.Register<TSystem>(Lifetime.Scoped).As<ITickSystem>();
        }

        if (typeof(ICallOnceSystem).IsAssignableFrom(systemType) &&
            !typeof(IUpdateSystem).IsAssignableFrom(systemType)) {
          registration = builder.Register<TSystem>(Lifetime.Scoped).As<ICallOnceSystem>();
        }

        return registration ?? throw new ArgumentException("Invalid system type");
      }
    }
  }
}
