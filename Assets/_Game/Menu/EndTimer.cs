using System;
using TMPro;
using UnityEngine;

public class EndTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerVariable;

    void Start()
    {
        TimeSpan currentTime = TimeSpan.FromSeconds(GameManager.TimeElapsed);
        timerVariable.text = $"{currentTime.Minutes}:{currentTime.Seconds}";
    }
}
