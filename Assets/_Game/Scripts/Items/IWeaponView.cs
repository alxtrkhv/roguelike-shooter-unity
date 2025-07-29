namespace Game.Items
{
  public interface IWeaponView
  {
    void SetWeapon(Weapon weapon);
    void PlayWeaponAnimation(float progress);
    void SetAttackCooldown(float progress);

    WeaponView ConvertToComponent()
    {
      return new() { Value = this, };
    }
  }
}
