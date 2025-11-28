using UnityEngine;

[CreateAssetMenu(fileName = "BombWeapon", menuName = "Scriptable Objects/Bomb Weapon")]
public class BombWeapon : BaseWeapon
{
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private int bombCount = 1;

    public override void Attack(GameObject gameObject, Vector2 direction) 
    {
        for (int i = 0; i < bombCount; i++)
        {
            Instantiate(bombPrefab, gameObject.transform.position, Quaternion.identity)
                .GetComponent<BombWeaponInstance>()
                .Init(direction)
            ;
        }
    }
}