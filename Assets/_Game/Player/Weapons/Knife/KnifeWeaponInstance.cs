using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class KnifeWeaponInstance : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool isActive = true;
    private int damage;
    private float lifeTime = .5f;

   
    public void Init(int dmg, Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * 180 / 3.14f - 90; //donne l'angle par rapport a ou il y a la souris le -90 c'est parce qu'il ce trompe de 90 degre
        Vector3 direction3 = Vector3.zero;
        direction3.z = angle;
        transform.eulerAngles = direction3;
        transform.position += new Vector3(direction.x,direction.y,0);
        damage = dmg;
        Destroy(gameObject, lifeTime);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActive)
            return;

        if (other.TryGetComponent<BaseMonster>(out var baseMonster))
            baseMonster.TakeDamage(damage);

    }


}
