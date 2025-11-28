#pragma warning disable IDE0090, IDE0060

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Transform playerTransform;
    public static Transform PlayerTransform => Instance.playerTransform;

    private float timeElapsed = 0f;
    public static float TimeElapsed => Instance.timeElapsed;

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

    private void Start() => JobSystemManager.Init();

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        JobSystemManager.MoveEnemies(playerTransform);
    }
}
