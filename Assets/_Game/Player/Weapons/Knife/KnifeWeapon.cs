using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

[CreateAssetMenu(fileName = "KnifeWeapon", menuName = "Scriptable Objects/Knife Weapon")]
public class KnifeWeapon : BaseWeapon
{
    [SerializeField] private KnifeWeaponInstance knifePrefab;
    [SerializeField] private int damage;

    public override void Attack(GameObject gameObject, Vector2 direction)
    {
        Transform gameObjectTransform = gameObject.transform;
        Instantiate(knifePrefab, gameObjectTransform.position, Quaternion.identity, gameObjectTransform).Init(damage,direction);
    }
}
