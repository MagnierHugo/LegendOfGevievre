using UnityEngine;

[CreateAssetMenu(fileName = "SwordWeapon", menuName = "Scriptable Objects/Sword Weapon")]
public class SwordWeapon : BaseWeapon
{
    [SerializeField] private GameObject swordPrefab;
    private int activeSwordCount = 0;

    public override void Attack(GameObject _, Vector2 __) 
    {
        for (; activeSwordCount < weaponLvl; activeSwordCount++)
            Instantiate(swordPrefab).GetComponent<SwordWeaponInstance>().Init(() => activeSwordCount--);
    }
}