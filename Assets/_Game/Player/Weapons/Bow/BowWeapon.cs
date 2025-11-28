using UnityEngine;

[CreateAssetMenu(fileName = "BowWeapon", menuName = "Scriptable Objects/Bow Weapon")]
public class BowWeapon : BaseWeapon
{
    [SerializeField] private Arrow arrowPrefab;

    public override void Attack(GameObject gameObject) 
    {
        Instantiate(arrowPrefab, gameObject.transform.position, Quaternion.identity)
            .Init(gameObject.GetComponent<PlayerMovement>())
        ;
    }

    public override void UpgradeInstance() {}
}