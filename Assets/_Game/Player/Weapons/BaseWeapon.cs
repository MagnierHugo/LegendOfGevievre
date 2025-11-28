using UnityEngine;

public abstract class BaseWeapon : ScriptableObject
{
    protected int weaponLvl = 1;
    public virtual void Attack(GameObject gameObjectAttacking, Vector2 direction) {}
    protected virtual void UpgradeImpl() { }

    public void Upgrade()
    {
        weaponLvl++;
        UpgradeImpl();
    }
}