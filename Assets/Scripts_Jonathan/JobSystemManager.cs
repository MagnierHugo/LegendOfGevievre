using System.Linq;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

public class JobSystemManager : MonoBehaviour
{
    [SerializeField] private Transform[] allEnemyTransforms;
    [SerializeField] private float globalSpeed = 2f;
    [SerializeField] private NativeArray<float3> nativePositions;
    [SerializeField] private NativeArray<float3> nativeVelocities;

    [SerializeField] private int maxEnemyCount = 2000;
    [SerializeField] private int currentActiveEnemies = 0;

    private float3 playerPos;

    private void Start()
    {
        nativePositions = new NativeArray<float3>(maxEnemyCount, Allocator.Persistent);       
        nativeVelocities = new NativeArray<float3>(maxEnemyCount, Allocator.Persistent);

        for (int i = 0; i < allEnemyTransforms.Length; i++)
            nativePositions[i] = allEnemyTransforms[i].position;
    }

    void Update()
    {
        playerPos = transform.position; // This script should be on the player
        float deltaTime = Time.deltaTime;

        var moveJob = new MoveEnemyJob
        {
            deltaTime = deltaTime,
            playerPosition = playerPos,
            speed = globalSpeed,
            positions = nativePositions,
            velocities = nativeVelocities
        };

        JobHandle handle = moveJob.Schedule(currentActiveEnemies, 64);

        handle.Complete();

        for (int i = 0; i < allEnemyTransforms.Length; i++)
            allEnemyTransforms[i].position = nativePositions[i];
    }

    private void OnDestroy()
    {
        if (nativePositions.IsCreated)
            nativePositions.Dispose();

        if (nativeVelocities.IsCreated)
            nativeVelocities.Dispose();
    }

    public void RegisterEnemy(Transform enemyTransform)
    {
        if (currentActiveEnemies >= maxEnemyCount)
            return;

        allEnemyTransforms[currentActiveEnemies] = enemyTransform;

        nativePositions[currentActiveEnemies] = enemyTransform.position;

        currentActiveEnemies++;
    }

    public void UnregisterEnemy(int indexToRemove)
    {
        int lastIndex = currentActiveEnemies - 1;

        allEnemyTransforms[indexToRemove] = allEnemyTransforms[lastIndex];
        nativePositions[indexToRemove] = nativePositions[lastIndex];

        allEnemyTransforms[lastIndex] = null;

        currentActiveEnemies--;
    }
}