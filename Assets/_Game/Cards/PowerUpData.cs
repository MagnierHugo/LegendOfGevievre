using UnityEngine;

[System.Serializable]
public class PowerUpData
{
    public string Name;
    [TextArea] public string Description;
    public BaseWeapon powerUp;
    public Sprite Icon;
    public int MaxLevel = 3;
    public int CurrentLevel = 0;
}
