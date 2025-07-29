using Game.AI;
using Game.Items;

namespace Game.Characters
{
  public class PlayerAuthoring : CharacterAuthoring
  {
    protected override float GetSpeed() => 2f;

    protected override void AddCharacterSpecificComponents(GameWorld.Entity entity)
    {
      entity.Add(new PlayerBehavior().ConvertToBehaviorComponent());
      entity.SetTag<Player>();
      entity.SetTag<LookingForNearestEnemy>();
    }

    protected override Equipment GetEquipment() => Weapon.Bow.ConvertToEquipment();
  }
}
