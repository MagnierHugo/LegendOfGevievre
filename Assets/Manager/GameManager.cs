using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private float timePassed = 0f;
    public static float TimePassed
    {
        get { return Instance.timePassed; }
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);   // Optional: persistent across scenes
    }

    private void Update()
    {
        timePassed += Time.deltaTime;
    }
}
