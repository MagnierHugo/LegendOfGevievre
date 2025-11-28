using UnityEngine;

[CreateAssetMenu(fileName = "SwordWeapon", menuName = "Scriptable Objects/Sword Weapon")]
public class SwordWeapon : BaseWeapon
{
    [SerializeField] private GameObject swordPrefab;

    public override void Attack(GameObject gameObject, Vector2 direction) 
    {
        Instantiate(swordPrefab);
    }
    public override void Upgrade() {}
}