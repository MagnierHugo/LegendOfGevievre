using UnityEngine;

[CreateAssetMenu(fileName = "AreaWeapon", menuName = "Scriptable Objects/Area Weapon")]
public class SwordWeapon : BaseWeapon
{
    [SerializeField] private GameObject areaPrefab;

    public override void CreateInstance() 
    {
        GameObject.Instantiate(areaPrefab);
    }
    public override void UpgradeInstance() {}
}