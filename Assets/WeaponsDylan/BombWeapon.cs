using UnityEngine;

[CreateAssetMenu(fileName = "BombWeapon", menuName = "Scriptable Objects/Bomb Weapon")]
public class BombWeapon : BaseWeapon
{
    [SerializeField] private GameObject bombPrefab;

    public override void CreateInstance() 
    {
        Instantiate(bombPrefab);
        Debug.Log("Instanciated bomb");
    }
    public override void UpgradeInstance() {}
}