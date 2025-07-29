using FFS.Libraries.StaticEcs;
using Game.Characters;
using Game.Movement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Game.Input
{
  public class ProcessPlayerInputSystem : IFrameSystem, IInitSystem
  {
    private InputActionMap _playerMap = null!;
    private InputAction _moveAction = null!;

    public void Init()
    {
      InputSystem.RegisterBindingComposite<ScreenJoystickComposite>();
      TouchSimulation.Enable();

      _playerMap = InputSystem.actions.FindActionMap("Player");
      _moveAction = _playerMap.FindAction("Move");

      _playerMap.Enable();
    }

    public void Update()
    {
      var moveInput = _moveAction.ReadValue<Vector2>();

      foreach (var entity in GameWorld.QueryEntities.For<TagAll<Player>>()) {
        entity.Put<TargetDirection>(new() { Value = moveInput, });
      }
    }
  }
}
