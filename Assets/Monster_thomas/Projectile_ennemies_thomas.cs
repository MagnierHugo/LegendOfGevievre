using NUnit.Framework;
using System.Drawing;
using UnityEngine;

public class Projectile_ennemies : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField] private float vit = 2;
    public void SetDirection(Transform player)
    {
        direction = player.position - transform.position;
    }
  
    // Update is called once per frame
    private void Update()
    {
        transform.position += Time.deltaTime * vit * direction;
    }
}
