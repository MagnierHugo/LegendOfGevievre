using UnityEngine;

[CreateAssetMenu(fileName = "SwordWeapon", menuName = "Scriptable Objects/Sword Weapon")]
public class SwordWeapon : BaseWeapon
{
    [SerializeField] private GameObject swordPrefab;
    private int activeSwordCount = 0;

    public override void Attack(GameObject gameObject, Vector2 direction) 
    {
        for (; activeSwordCount < weaponLvl; activeSwordCount++)
            Instantiate(swordPrefab).GetComponent<SwordWeaponInstance>().Init(() => activeSwordCount--);
    }
}