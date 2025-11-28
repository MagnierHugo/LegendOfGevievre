using UnityEngine;

//public enum PowerUpType { Weapon, WeaponUpgrade, StatBoost, Passive }

[System.Serializable]
public class PowerUpData
{
    public string Name;
    [TextArea] public string Description;
    //public PowerUpType Type;
    public Sprite Icon;
    public int MaxLevel = 5;
    public int CurrentLevel = 0;
}

