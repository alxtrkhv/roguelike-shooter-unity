using Game.HealthManagement;
using Game.Items;
using Game.Movement;
using PrimeTween;
using TMPro;
using UnityEngine;

namespace Game.Characters
{
  public abstract class CharacterAuthoring : Authoring, IHealthView, IWeaponView
  {
    [SerializeField]
    private SpriteRenderer? _body;

    [SerializeField]
    private TMP_Text? _healthText;

    [SerializeField]
    private TMP_Text? _weaponText;

    private Sequence _attackSequence;

    protected override GameWorld.Entity OnBake()
    {
      var entity = GameWorld.Entity.New(
        new Speed { Value = GetSpeed(), }
      );

      entity.SetTag<Character>();
      AddCharacterSpecificComponents(entity);

      var equipment = GetEquipment();
      entity.Add(equipment);

      var health = new Health() { Value = 100f, MaxValue = 100f, };

      entity.SetTag<Alive>();
      entity.Add(health);

      var healthView = GetComponentInChildren<IHealthView>();
      if (healthView != null) {
        entity.Add(healthView.ConvertToComponent());
        healthView.SetHealth(health.Value, health.MaxValue);
        healthView.SetAlive(true);
      }

      var weaponView = GetComponentInChildren<IWeaponView>();
      if (weaponView != null) {
        entity.Add(weaponView.ConvertToComponent());
        weaponView.SetWeapon(equipment.Weapon);
      }

      return entity;
    }

    public virtual void SetHealth(float current, float max)
    {
      if (_healthText == null) {
        return;
      }

      _healthText.text = $"{current:F0}/{max:F0}";
    }

    public virtual void SetAlive(bool value)
    {
      if (_body == null) {
        return;
      }

      var color = _body.color;
      color.a = value ? 1f : 0.5f;
      _body.color = color;
    }

    public void SetWeapon(Weapon weapon)
    {
      if (_weaponText == null) {
        return;
      }

      if (weapon.Name == Weapon.Bow.Name) {
        _weaponText.text = "Bow";
      } else if (weapon.Name == Weapon.Sword.Name) {
        _weaponText.text = "Sword";
      } else {
        _weaponText.text = "";
      }
    }

    public void PlayWeaponAnimation(float progress)
    {
      if (_weaponText == null) {
        return;
      }

      if (_attackSequence.isAlive) {
        return;
      }

      var scale = 1.25f;
      _attackSequence = Sequence.Create()
        .Chain(Tween.Scale(_weaponText.transform, scale, 0.25f))
        .Chain(Tween.Scale(_weaponText.transform, 1f, 0.25f));
    }

    public void SetAttackCooldown(float progress)
    {
      if (_weaponText == null) {
        return;
      }

      var color = _weaponText.color;
      color.a = 0.5f + (progress * 0.5f);
      _weaponText.color = color;
    }

    protected abstract float GetSpeed();
    protected abstract void AddCharacterSpecificComponents(GameWorld.Entity entity);
    protected abstract Equipment GetEquipment();
  }
}
