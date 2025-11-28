using System;
using Unity.VisualScripting;
using UnityEngine;

public class BombWeaponInstance : MonoBehaviour
{
    private Vector3 targetPosition = Vector3.zero;
    private Vector2 explosionPosition = Vector2.zero;

    private const float SHOOTS_ELAPSED_TIME = 2.0f;
    private const float GRAVITY = 9.81f;
    private float jumpForce = 4.0f;
    private float projectileSpeed = 5.0f;
    private float timer = SHOOTS_ELAPSED_TIME / (level * 1.05f);

    private const int MAX_DIRECTIONS = 8;
    private static int range = 20;
    private static int damage = 50;
    private static int level = 1;

    private bool isAlive = true;
    private bool isMultiEnabled = true;
    private bool isClone = false;

    GameObject explosion;
    
    // Only when multi
    private GameObject[] multipleObjects    = null;
    private GameObject[] multipleExplosions = null;
    private Vector3[] multiplePositions     = null;
    private Vector2[] explosionsPositions   = null;
    private bool[] isAliveAt                = null;
    public void Start()
    {
        InitInstance();
    }

    static bool isFirstPass = true;
    public void InitInstance()
    {
        if (isClone) return;
        isAlive = true;
        explosion = GameObject.Find("Explosion");
        explosion.SetActive(false);
        targetPosition = transform.position + Vector3.right * 8 + Vector3.back * 8;
        Debug.Log("Target position is : " + targetPosition.ToString());
        isFirstPass = false;
        if (isMultiEnabled)
        {
            multipleObjects = new GameObject[MAX_DIRECTIONS];
            multipleExplosions = new GameObject[MAX_DIRECTIONS];
            multiplePositions = new Vector3[MAX_DIRECTIONS];
            isAliveAt = new bool[MAX_DIRECTIONS];
            for (int i = 0; i < multipleObjects.Length; i++) 
            {
                multiplePositions[i] = targetPosition + Vector3.right * 0.2f * (i + 1);
                multipleObjects[i] = Instantiate(gameObject);
                multipleObjects[i].GetComponent<BombWeaponInstance>().isClone = true;
                multipleExplosions[i] = Instantiate(explosion);
                isAliveAt[i] = true;    
            }
        }
    }
    
    public void Update()
    {
        ParabollicMovement();
    }

    public void ParabollicMovement()
    {
        void setExplosion(bool isMulti = false, int index = 0)
        {
            if (!isMulti)
            {
                explosion.SetActive(true);
                explosionPosition = new Vector2(UnityEngine.Random.Range(0, 10), UnityEngine.Random.Range(0, 10));
                Explode();
            }
            else
            {
                multipleExplosions[index].SetActive(true);
                explosionPosition = new Vector2(UnityEngine.Random.Range(0, 10), UnityEngine.Random.Range(0, 10));
                Explode(isMulti, index);
            }
        }
        void reset() {
            Destroy(gameObject);
            isAlive = false;
            timer = SHOOTS_ELAPSED_TIME;
        } 
        timer -= Time.deltaTime;
        if (timer < 0 && isAlive)
        {
            transform.position += new Vector3(projectileSpeed * Time.deltaTime, jumpForce * Time.deltaTime, 0);
            jumpForce -= Time.deltaTime * GRAVITY;
            if (transform.position.y <= targetPosition.y && transform.position.x <= targetPosition.x)
            {
                setExplosion();
                reset();
            }
            if (isMultiEnabled)
            {
                for(int i = 0; i < multipleObjects?.Length; i++) 
                {
                    if (multipleObjects[i] == null)
                        break;
                    multipleObjects[i].transform.position += new Vector3(projectileSpeed * Time.deltaTime, jumpForce * Time.deltaTime, 0);
                }
            }
            // Out of bounds (screen dims wise)
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            if (screenPosition.x > Screen.width || screenPosition.x < 0 || screenPosition.y < 0 || screenPosition.y > Screen.height)
            {
                Debug.Log("You missed :skull:");
                reset();
            }
        }
    }

    public void Upgrade()
    {
        damage += 10;
        range += 5;
        level++;
    }

    private void Explode(bool isMulti = false, int index = 0)
    {
        // TODO: Explosion visual
        if(!isMulti)
            explosion.transform.position = explosionPosition;
        else
            multipleExplosions[index].transform.position = explosionPosition;
        Debug.Log("Explosion at " + explosionPosition.ToString() + ", it's a decoy :D 1!!!!1!1!1!1!!");
        Debug.Log("The real me is at : " + transform.position.ToString() + " !!111!1!1!1!!1!1");
    }
}