using UnityEngine;

namespace Game.Characters
{
  public class EnemyPool : MonoBehaviour
  {
    [SerializeField]
    private EnemyAuthoring _prefab = null!;

    [SerializeField]
    private Transform? _rootFoDisabled;

    private void Awake()
    {

    }

    private Transform RootForDisabled()
    {
      var disabled = _rootFoDisabled;
      if (disabled is not null) {
        return disabled;
      }

      return transform;
    }
  }
}
