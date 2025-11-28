using UnityEngine;
using UnityEngine.UIElements;

public class PickableManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] PlayerController player = null;

    [Header("Xp Orbs")]
    [SerializeField] GameObject smallXpOrb = null;
    [SerializeField] GameObject mediumXpOrb = null;
    [SerializeField] GameObject largeXpOrb = null;

    [Header("Pickable Prefab")]
    [SerializeField] GameObject healPotion = null;
    [SerializeField] GameObject shield = null;

    [Header("Pickable Parent")]
    [SerializeField] Transform xpOrbParent = null;
    [SerializeField] Transform healPotionParent = null;
    [SerializeField] Transform shieldParent = null;

    
    public void SpawnXpOrb(Vector3 position)
    {
        float random = Random.value;

        if (random <= 0.7) // 70% chance of small orb
            Instantiate(smallXpOrb, position, Quaternion.identity, xpOrbParent);

        else if (random <= 0.95) // 25% chance of medium orb
            Instantiate(mediumXpOrb, position, Quaternion.identity, xpOrbParent);

        else if (random <= 1) // 5% chance of large orb
            Instantiate(largeXpOrb, position, Quaternion.identity, xpOrbParent);
    }

    public void SpawnHeal(Vector3 position)
    {
        Instantiate(healPotion, position, Quaternion.identity, healPotionParent);
    }

    public void SpawnShield(Vector3 position)
    {
        Instantiate(shield, position, Quaternion.identity, shieldParent);
    }
}