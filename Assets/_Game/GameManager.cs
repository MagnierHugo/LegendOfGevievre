#pragma warning disable IDE0090, IDE0060

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    static GameManager()
    {
        GameObject gameObject = new GameObject("GameManager");
        Instance = gameObject.AddComponent<GameManager>();
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] private Transform playerTransform;

    private float timeElapsed = 0f;
    public static float TimeElapsed => Instance.timeElapsed;

    private void Start() => JobSystemManager.Init();

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        JobSystemManager.MoveEnemies(playerTransform);
    }
}
