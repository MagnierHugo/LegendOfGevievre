using UnityEngine;

[CreateAssetMenu(fileName = "KnifeWeapon", menuName = "Scriptable Objects/Knife Weapon")]
public class KnifeWeapon : BaseWeapon
{

    [SerializeField] private KnifeWeaponInstance preFab;
    [SerializeField] private int damage;
    public override void Attack(GameObject gameObject, Vector2 direction)
    {
        Instantiate(preFab, gameObject.transform.position, Quaternion.LookRotation(direction)).Init(damage);
    }
    public override void UpgradeInstance() { }

}
