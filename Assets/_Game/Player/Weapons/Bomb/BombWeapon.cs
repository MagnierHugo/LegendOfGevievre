using UnityEngine;

[CreateAssetMenu(fileName = "BombWeapon", menuName = "Scriptable Objects/Bomb Weapon")]
public class BombWeapon : BaseWeapon
{
    [SerializeField] private GameObject bombPrefab;

    public override void Attack(GameObject gameObject, Vector2 direction) 
    {
        for (int i = 0; i < weaponLvl; i++)
        {
            // Slightly shift direction with multiple bombs
            direction += new Vector2(i * 0.1f, -i * 0.1f);
            Instantiate(bombPrefab, gameObject.transform.position, Quaternion.identity)
                .GetComponent<BombWeaponInstance>()
                .Init(direction, weaponLvl)
            ;
        }
    }

    protected override void UpgradeImpl()
    {
        // Higher damage and range
        bombPrefab.
            GetComponent<BombWeaponInstance>().
            Upgrade()
        ;
    }
}