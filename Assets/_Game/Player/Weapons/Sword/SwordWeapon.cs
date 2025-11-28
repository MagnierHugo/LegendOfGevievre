using UnityEngine;

[CreateAssetMenu(fileName = "SwordWeapon", menuName = "Scriptable Objects/Sword Weapon")]
public class SwordWeapon : BaseWeapon
{
    [SerializeField] private GameObject swordPrefab;
    private int activeSwordCount = 0;

<<<<<<< HEAD
    public override void Attack(GameObject gameObject, Vector2 direction, int upgradeLevel) 
=======
    public override void Attack(GameObject _, Vector2 __) 
>>>>>>> b5052247472663c815f22e5224bee838259f62e8
    {
        for (; activeSwordCount < weaponLvl; activeSwordCount++)
            Instantiate(swordPrefab).GetComponent<SwordWeaponInstance>().Init(() => activeSwordCount--);
    }
}