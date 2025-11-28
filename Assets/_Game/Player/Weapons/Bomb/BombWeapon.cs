using UnityEngine;

[CreateAssetMenu(fileName = "BombWeapon", menuName = "Scriptable Objects/Bomb Weapon")]
public class BombWeapon : BaseWeapon
{
    [SerializeField] private GameObject bombPrefab;

    public override void Attack(GameObject gameObject, Vector2 baseDir)
    {
        int count = BombWeaponInstance.level;
        float step = 360f / count;  // how many degrees between bombs

        for (int i = 0; i < count; i++)
        {
            // rotation angle for this bomb
            float angle = i * step;

            // rotate base direction by angle
            Vector2 dir = Quaternion.Euler(0, 0, angle) * baseDir;

            // spawn projectile
            Instantiate(bombPrefab, gameObject.transform.position, Quaternion.identity)
                .GetComponent<BombWeaponInstance>()
                .Init(dir.normalized, weaponLvl);
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