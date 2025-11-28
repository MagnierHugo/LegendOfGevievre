using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "ScytheWeapon", menuName = "Scriptable Objects/Scythe Weapon")]
public class ScytheWeapon : BaseWeapon
{
    [SerializeField] private GameObject scythePrefab;

    public override void Attack(GameObject gameObject, Vector2 direction)
    {
        Instantiate(scythePrefab, gameObject.transform.position, Quaternion.identity, gameObject.transform)
            .GetComponent<ScytheProjectile>()
            .Init(direction);
    }
}