using UnityEngine;

public class HealPotion
{
    [SerializeField] private int healValue = 0;

    public int GetHealValue()
    {
        return healValue;
    }
}