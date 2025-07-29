using FFS.Libraries.StaticEcs;

namespace Game.HealthManagement
{
  public struct AttackCooldown : IComponent
  {
    public float RemainingTime;
  }
}
