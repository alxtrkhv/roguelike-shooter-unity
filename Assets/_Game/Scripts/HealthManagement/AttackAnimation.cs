using FFS.Libraries.StaticEcs;

namespace Game.HealthManagement
{
  public struct AttackAnimation : IComponent
  {
    public float RemainingTime;
    public float Duration;

    public AttackAnimation(float duration)
    {
      Duration = duration;
      RemainingTime = duration;
    }
  }
}
