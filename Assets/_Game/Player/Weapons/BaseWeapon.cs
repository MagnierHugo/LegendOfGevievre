using UnityEngine;

public abstract class BaseWeapon : ScriptableObject
{
    public int upgradeLevel = 0;
    public virtual void Attack(GameObject gameObjectAttacking, Vector2 direction, int upgradeLevel = 0) {}
    public virtual void Upgrade() {}
}