using System.Drawing;
using UnityEngine;

public class Projectile_ennemies : MonoBehaviour
{
    [SerializeField] private Transform _projectile;
    private float pos_x;
    private float pos_y;
    private float[] vecteur_direction = new float[2];

    public void SetDirection(Vector3 player)
    {
        vecteur_direction = new float[2] { player.x - _projectile.position.x, player.y - _projectile.position.y };
    }
  
    // Update is called once per frame
    private void Update()
    {
        Debug.Log("test");
        _projectile.position += new Vector3(vecteur_direction[0] * 1 * Time.deltaTime, 0, 0);
        _projectile.position += new Vector3(0, vecteur_direction[1] * 1 * Time.deltaTime, 0);
    }
}
