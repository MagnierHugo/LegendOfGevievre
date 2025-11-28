using UnityEngine;

[CreateAssetMenu(fileName = "BombWeapon", menuName = "Scriptable Objects/Bomb Weapon")]
public class BombWeapon : BaseWeapon
{
    [SerializeField] private GameObject bombPrefab;

    public override void Attack(GameObject gameObject, Vector2 direction) 
    {
        Instantiate(bombPrefab);
    }
}