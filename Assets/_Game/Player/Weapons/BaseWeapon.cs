using UnityEngine;

public abstract class BaseWeapon : ScriptableObject
{
    public virtual void Attack(GameObject gameObjectAttacking, Vector2 direction) {}
    public virtual void Upgrade() {}
}