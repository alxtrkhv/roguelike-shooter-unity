using Game.HealthManagement;
using Game.Items;
using Game.Movement;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Characters
{
  public abstract class CharacterAuthoring : Authoring, IHealthView
  {
    [SerializeField]
    private SpriteRenderer? _body;

    [SerializeField]
    private TMP_Text? _healthText;

    protected override GameWorld.Entity OnBake()
    {
      var entity = GameWorld.Entity.New(
        new Speed { Value = GetSpeed(), }
      );

      entity.SetTag<Character>();
      AddCharacterSpecificComponents(entity);
      entity.Add(GetEquipment());

      var health = new Health() { Value = 100f, MaxValue = 100f, };

      entity.SetTag<Alive>();
      entity.Add(health);

      var healthView = GetComponentInChildren<IHealthView>();
      if (healthView != null) {
        entity.Add(healthView.ConvertToComponent());
        healthView.SetHealth(health.Value, health.MaxValue);
        healthView.SetAlive(true);
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

    protected abstract float GetSpeed();
    protected abstract void AddCharacterSpecificComponents(GameWorld.Entity entity);
    protected abstract Equipment GetEquipment();
  }
}
