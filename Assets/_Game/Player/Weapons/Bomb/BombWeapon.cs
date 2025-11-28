using UnityEngine;

[CreateAssetMenu(fileName = "BombWeapon", menuName = "Scriptable Objects/Bomb Weapon")]
public class BombWeapon : BaseWeapon
{
    [SerializeField] private GameObject bombPrefab;

    public override void Attack(GameObject gameObject, Vector2 direction) 
    {
        for (int i = 0; i < weaponLvl; i++)
        {
            Instantiate(bombPrefab, gameObject.transform.position, Quaternion.identity)
                .GetComponent<BombWeaponInstance>()
                .Init(direction, weaponLvl)
            ;
        }
    }

    protected override void UpgradeImpl()
    {

    }
}