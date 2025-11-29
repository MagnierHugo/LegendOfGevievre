using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{

    [Serializable]
    private struct SpawnData
    {
        public GameObject prefab;
        public float initialSpawnDelay;
    }

    [SerializeField] private List<SpawnData> spawnDataList = new List<SpawnData>();
    [SerializeField] private Vector2Int mapBottomLeft = new Vector2Int(-32, -32);
    [SerializeField] private Vector2Int mapTopRight = new Vector2Int(32, 32);
    [SerializeField] private float minSpawnRadius = 10f;
    [SerializeField] private float maxSpawnRadius = 20f;
    [Tooltip("Time between enemy spawn in seconds")]
    [SerializeField] private float spawnDelay = 2f;
    [SerializeField] private float minSpawnDelay = .2f;

    private float spawnTimer;

    private void Update()
    {
        if (spawnTimer >= spawnDelay)
        {
            GameObject ennemy = GetEnemyToSpawn();
            Vector2 position = Random.insideUnitCircle.normalized * Random.Range(minSpawnRadius, maxSpawnRadius) + (Vector2)GameManager.PlayerTransform.position;
            position = new Vector2(
                position.x <= mapBottomLeft.x ? mapBottomLeft.x : position.x >= mapTopRight.x ? mapTopRight.x : position.x,
                position.y <= mapBottomLeft.y ? mapBottomLeft.y : position.y >= mapTopRight.y ? mapTopRight.y : position.y
            );
            Instantiate(ennemy, position, Quaternion.identity, transform);
            spawnTimer = 0f;
            if (minSpawnDelay < spawnDelay)
                spawnDelay = Mathf.Max(spawnDelay - (spawnDelay / 30f), minSpawnDelay);
        }

        spawnTimer += Time.deltaTime;
    }

    private GameObject GetEnemyToSpawn()
    {
        while (true)
        {
            SpawnData spawnData = spawnDataList[Random.Range(0, spawnDataList.Count)];
            if (GameManager.TimeElapsed >= spawnData.initialSpawnDelay)
                return spawnData.prefab;
        }

    }
}
