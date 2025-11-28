using UnityEngine;

public class KnifeWeapon : BaseWeapon
{

    [SerializeField] private KnifeWeaponInstance preFab;
    public override void Attack(GameObject gameObject)
    {
        Instantiate(preFab, gameObject.transform.position, Quaternion.identity);
    }
    public override void UpgradeInstance() { }

}
