using UnityEngine;

[CreateAssetMenu(fileName = "BowWeapon", menuName = "Scriptable Objects/Bow Weapon")]
public class BowWeapon : BaseWeapon
{
    [SerializeField] private GameObject bowPrefab;

    public override void CreateInstance() 
    {
        Instantiate(bowPrefab);
    }
    public override void UpgradeInstance() {}
}