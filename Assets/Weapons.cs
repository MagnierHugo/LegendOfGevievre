using UnityEngine;

[CreateAssetMenu(fileName = "Weapons", menuName = "Scriptable Weapons/Weapons")]
public class Weapons : ScriptableObject
{
    [SerializeField] private int damage;
    [SerializeField] private int range;
}