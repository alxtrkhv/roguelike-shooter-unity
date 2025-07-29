using Game.AI;
using Game.Items;

namespace Game.Characters
{
  public class EnemyAuthoring : CharacterAuthoring
  {
    protected override float GetSpeed() => 1.5f;

    protected override void AddCharacterSpecificComponents(GameWorld.Entity entity)
    {
      entity.Add(new Behavior { Value = new EnemyBehavior(), });
      entity.SetTag<NPC, Enemy>();
    }

    protected override Equipment GetEquipment() => Weapon.Sword.ConvertToEquipment();
  }
}
