namespace Game.HealthManagement
{
  public interface IHitView
  {
    void Hit();

    HitView ConvertToComponent()
    {
      return new() { Value = this, };
    }
  }
}
