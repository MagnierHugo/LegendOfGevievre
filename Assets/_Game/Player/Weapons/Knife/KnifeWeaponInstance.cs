using UnityEngine;

public class KnifeWeaponInstance : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool isActive = true;
    [SerializeField] private int damage;
    private void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActive)
            return;

        if (other.TryGetComponent<BaseMonster>(out var baseMonster))
            baseMonster.TakeDamage(damage);

    }


}
