using System.Collections;

using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float stutterDuration = 1;
    private WaitForSeconds stutterDuration_;
    [SerializeField] private float liveDuration = .5f;
    private WaitForSeconds liveDuration_;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    private Vector3 velocity;
    private bool isActive = true;
    public void Init(PlayerMovement playerMovement)
    {
        velocity = playerMovement.Velocity == Vector2.zero ?
            Vector3.right * speed :
            playerMovement.Velocity.normalized * speed
        ;
        stutterDuration_ = new WaitForSeconds(stutterDuration);
        liveDuration_ = new WaitForSeconds(liveDuration);
        Destroy(gameObject, lifeTime);
        StartCoroutine(CycleStutterAndLive());
    }

    private void Update()
    {
        if (isActive)
            transform.position += velocity * Time.deltaTime;
    }
    
    private IEnumerator CycleStutterAndLive()
    {
        while (true)
        {
            yield return isActive ? liveDuration_ : stutterDuration_;
            isActive = !isActive;
            print("flipped");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActive)
            return;

        if (other.TryGetComponent<BaseMonster>(out var baseMonster))
            baseMonster.TakeDamage(damage);
    }

}