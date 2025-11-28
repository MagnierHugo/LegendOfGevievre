
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField] private float vit = 2;
    public void SetDirection(Transform player)
    {
        direction = player?.position ?? Vector3.zero - transform.position;
    }
  
    // Update is called once per frame
    private void Update()
    {
        transform.position += Time.deltaTime * vit * direction;
    }
}
