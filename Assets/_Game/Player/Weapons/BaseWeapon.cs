using UnityEngine;

public abstract class BaseWeapon : ScriptableObject
{
<<<<<<< HEAD
    public int upgradeLevel = 0;
    public virtual void Attack(GameObject gameObjectAttacking, Vector2 direction, int upgradeLevel = 0) {}
    public virtual void Upgrade() {}
=======
    protected int weaponLvl = 1;

    public virtual void Attack(GameObject gameObjectAttacking, Vector2 direction) {}
    protected virtual void UpgradeImpl() { }

    public void Upgrade()
    {
        weaponLvl++;
        UpgradeImpl();
    }
>>>>>>> b5052247472663c815f22e5224bee838259f62e8
}