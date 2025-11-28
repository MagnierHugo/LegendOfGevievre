using UnityEngine;
using System;

public class PlayerXP : MonoBehaviour
{
    public int CurrentXP { get; private set; }

    public static event Action<int> OnXPChanged;

    public void EarnXP(int xpAmount)
    {
        CurrentXP += xpAmount;

        OnXPChanged?.Invoke(CurrentXP);

        Debug.Log($"Earned {xpAmount} XP. Total XP: {CurrentXP}");
    }
}