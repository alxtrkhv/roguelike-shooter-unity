using FFS.Libraries.StaticEcs;

namespace Game.HealthManagement
{
  public struct AttackCooldown : IComponent
  {
    public float RemainingTime;
    public float Duration;

    public AttackCooldown(float duration)
    {
      Duration = duration;
      RemainingTime = duration;
    }
  }
}
