using UnityEngine;

[CreateAssetMenu(fileName = "monster", menuName = "ScriptableObjects/ScriptableMonster", order = 1)]
public class ScriptableMonster : ScriptableObject
{
    public string prefabName;

    public int numberOfPrefabsToCreate;
    public Vector3[] spawnPoints;
}