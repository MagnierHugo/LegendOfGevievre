using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

[BurstCompile]
public struct MoveEnemyJob : IJobParallelFor
{
    [ReadOnly] public float deltaTime;
    [ReadOnly] public float3 playerPosition;
    [ReadOnly] public float speed;

    public NativeArray<float3> positions;
    public NativeArray<float3> velocities;

    public void Execute(int index)
    {
        float3 currentPosition = positions[index];

        float3 direction = playerPosition - currentPosition;

        float3 velocity = math.normalize(direction) * speed;

        float3 newPosition = currentPosition + (velocity * deltaTime);

        positions[index] = newPosition;
    }
}