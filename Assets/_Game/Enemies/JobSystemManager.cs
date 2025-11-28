//using Unity.Collections;
//using Unity.Jobs;
//using Unity.Mathematics;
//using UnityEngine;
//using System.Collections.Generic;

//public static class JobSystemManager
//{
//    private static List<Transform> allEnemyTransforms = new List<Transform>();
//    private static float globalSpeed = 2f;
//    private static NativeArray<float3> positions;
//    private static NativeArray<float3> velocities;

//    private static int maxEnemyCount = 2000;
//    private static int currentActiveEnemies = 0;

//    private static float3 playerPos;

//    public static void Init()
//    {
//        positions = new NativeArray<float3>(maxEnemyCount, Allocator.Persistent);
//        velocities = new NativeArray<float3>(maxEnemyCount, Allocator.Persistent);
//    }

//    public static void MoveEnemies(Transform playerTransform)
//    {
//        playerPos = playerTransform.position;
//        float deltaTime = Time.deltaTime;

//        var moveJob = new MoveEnemyJob()
//        {
//            deltaTime = deltaTime,
//            playerPosition = playerPos,
//            speed = globalSpeed,
//            positions = positions,
//            velocities = velocities
//        };

//        JobHandle handle = moveJob.Schedule(currentActiveEnemies, 64);

//        handle.Complete();

//        for (int i = 0; i < allEnemyTransforms.Count; i++)
//            allEnemyTransforms[i].position = positions[i];
//    }

//    public static void DestroyLists()
//    {
//        if (positions.IsCreated)
//            positions.Dispose();
//        if (velocities.IsCreated)
//            velocities.Dispose();
//    }

//    public static void RegisterEnemy(Transform enemyTransform)
//    {
//        if (currentActiveEnemies >= maxEnemyCount)
//            return;

//        allEnemyTransforms.Add(enemyTransform);

//        positions[currentActiveEnemies] = enemyTransform.position;

//        currentActiveEnemies++;
//    }

//    public static void UnregisterEnemy(Transform enemyTransform)
//    {
//        if (!allEnemyTransforms.Contains(enemyTransform))
//            return;

//        int indexToRemove = allEnemyTransforms.IndexOf(enemyTransform);
//        int lastIndex = currentActiveEnemies - 1;

//        allEnemyTransforms[indexToRemove] = allEnemyTransforms[lastIndex];
//        positions[indexToRemove] = positions[lastIndex];

//        allEnemyTransforms[lastIndex] = null;

//        currentActiveEnemies--;
//    }
//}