using UnityEngine;

[CreateAssetMenu(fileName = "SwordWeapon", menuName = "Scriptable Objects/Sword Weapon")]
public class SwordWeapon : BaseWeapon
{
    [SerializeField] private GameObject swordPrefab;

    public override void Attack(GameObject gameObject) 
    {
        Instantiate(swordPrefab);
    }
    public override void UpgradeInstance() {}
}