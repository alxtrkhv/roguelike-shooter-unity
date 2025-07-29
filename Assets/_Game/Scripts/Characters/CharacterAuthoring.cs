using Game.Items;
using Game.Movement;

namespace Game.Characters
{
  public abstract class CharacterAuthoring : Authoring
  {
    protected override GameWorld.Entity OnBake()
    {
      var entity = GameWorld.Entity.New(
        new Speed { Value = GetSpeed(), }
      );

      entity.SetTag<Character>();
      AddCharacterSpecificComponents(entity);
      entity.Add(GetEquipment());

      return entity;
    }

    protected abstract float GetSpeed();
    protected abstract void AddCharacterSpecificComponents(GameWorld.Entity entity);
    protected abstract Equipment GetEquipment();
  }
}
