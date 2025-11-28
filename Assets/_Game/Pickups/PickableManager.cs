
using UnityEngine;


public class PickableManager : MonoBehaviour
{
    [Header("Pickable Prefab")]
    [SerializeField] private GameObject healPotion = null;
    [SerializeField] private GameObject shield = null;

    [Header("Pickable Parent")]
    [SerializeField] private Transform healPotionParent = null;
    [SerializeField] private Transform shieldParent = null;

    public void SpawnHeal(Vector3 position)
    {
        Instantiate(healPotion, position, Quaternion.identity, healPotionParent);
    }

    public void SpawnShield(Vector3 position)
    {
        Instantiate(shield, position, Quaternion.identity, shieldParent);
    }
}