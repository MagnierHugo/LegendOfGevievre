using System;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "ScytheWeapon", menuName = "Scriptable Objects/Scythe Weapon")]
public class ScytheWeapon : BaseWeapon
{
    [SerializeField] private GameObject scythePrefab;

    public override void Attack(GameObject gameObject, Vector2 direction, int upgradeLevel)
    {
        switch (upgradeLevel)
        {
            case 0:
                Instantiate(scythePrefab, gameObject.transform.position, Quaternion.identity, gameObject.transform)
                    .GetComponent<ScytheProjectile>()
                    .Init(direction);
                break;

            case 1:
                Instantiate(scythePrefab, gameObject.transform.position, Quaternion.identity, gameObject.transform)
                    .GetComponent<ScytheProjectile>()
                    .Init(direction);

                Instantiate(scythePrefab, gameObject.transform.position, Quaternion.identity, gameObject.transform)
                    .GetComponent<ScytheProjectile>()
                    .Init(-direction);
                break;

            case >= 2:
                GameObject scythe = Instantiate(scythePrefab, gameObject.transform.position, Quaternion.identity, gameObject.transform);
                scythe.GetComponent<ScytheProjectile>().Init(direction);
                scythe.transform.localScale = new Vector3(2f, 2f, 1f);

                GameObject scythe2 = Instantiate(scythePrefab, gameObject.transform.position, Quaternion.identity, gameObject.transform);
                scythe2.GetComponent<ScytheProjectile>().Init(-direction);
                scythe2.transform.localScale = new Vector3(2f, 2f, 1f);
                break;

            default:
                break;
        }
    }
    public override void Upgrade()
    {
        base.Upgrade();
        upgradeLevel += 1;
    }
}