using UnityEngine;

public abstract class BaseWeapon : ScriptableObject
{
    public virtual void Attack(GameObject gameObjectAttacking) {}
    public virtual void Upgrade() {}
}