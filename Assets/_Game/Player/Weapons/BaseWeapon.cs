using UnityEngine;

public abstract class BaseWeapon : ScriptableObject
{
    public virtual void CreateInstance() {}
    public virtual void UpgradeInstance() {}
}