#pragma warning disable IDE0090, IDE0060

using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static Transform PlayerTransform => PlayerMovement.Instance.transform;

    private float timeElapsed = 0f;
    public static float TimeElapsed => Instance.timeElapsed;

    public static bool GamePaused { get; private set; }


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update() => timeElapsed += Time.deltaTime;

    public void TogglePause()
    {
        GamePaused = !GamePaused;
        Time.timeScale = GamePaused ? 0f : 1f;
    }

    public static void Reset_()
    {
        if(Instance != null)
            Instance.timeElapsed = 0;
    }
}
