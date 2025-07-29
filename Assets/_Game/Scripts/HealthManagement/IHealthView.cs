namespace Game.HealthManagement
{
  public interface IHealthView
  {
    void SetHealth(float current, float max);
    void SetAlive(bool value);

    HealthView ConvertToComponent()
    {
      return new() { Value = this, };
    }
  }
}
