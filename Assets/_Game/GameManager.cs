#pragma warning disable IDE0090, IDE0060

using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager()
    {
        GameObject gameObject = new GameObject("GameManager");
        Instance = gameObject.AddComponent<GameManager>();
        DontDestroyOnLoad(gameObject);
    }

    public static GameManager Instance { get; private set; }

    private float timePassed = 0f;
    public static float TimeElapsed => Instance.timePassed;

    private void Update()
    {
        timePassed += Time.deltaTime;
    }
}
