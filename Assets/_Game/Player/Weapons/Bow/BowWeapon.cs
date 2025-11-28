using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "BowWeapon", menuName = "Scriptable Objects/Bow Weapon")]
public class BowWeapon : BaseWeapon
{
    [SerializeField] private Arrow arrowPrefab;

    private int numberOfArrowForChain = 10;

    public override void Attack(GameObject gameObject, Vector2 direction) 
    {
        MonoBehaviour coroutineRunner = gameObject.GetComponent<PlayerMovement>();

        switch (weaponLvl)
        {
            case 2:
                Instantiate(arrowPrefab, gameObject.transform.position, Quaternion.identity)
                    .Init(gameObject.GetComponent<PlayerMovement>(), direction);
                break;

            case 1:
                Quaternion rightRotation = Quaternion.Euler(0, 0, 45f);
                Quaternion leftRotation = Quaternion.Euler(0, 0, -45f);
                Vector2 rightRotatedDirection = rightRotation * direction;
                Vector2 leftRotatedDirection = leftRotation * direction;

                Instantiate(arrowPrefab, gameObject.transform.position, Quaternion.identity)
                    .Init(gameObject.GetComponent<PlayerMovement>(), direction);

                Instantiate(arrowPrefab, gameObject.transform.position, Quaternion.identity)
                    .Init(gameObject.GetComponent<PlayerMovement>(), rightRotatedDirection);

                Instantiate(arrowPrefab, gameObject.transform.position, Quaternion.identity)
                    .Init(gameObject.GetComponent<PlayerMovement>(), leftRotatedDirection);
                break;

            case >= 3:
                if (coroutineRunner != null)
                    coroutineRunner.StartCoroutine(ChainArrow(gameObject, direction));
                break;

            default:
                break;
        }
    }

    private IEnumerator ChainArrow(GameObject gameObject, Vector2 direction)
    {
        float timeBetweenArrows = 1f/numberOfArrowForChain;
        float angleStep = 360f / numberOfArrowForChain;

        for (int i = 0; i < numberOfArrowForChain; i++)
        {
            float currentAngle = angleStep * i;
            Quaternion rotation = Quaternion.Euler(0, 0, currentAngle);
            Vector2 rotatedDirection = rotation * direction;

            Instantiate(arrowPrefab, gameObject.transform.position, Quaternion.identity)
                .Init(gameObject.GetComponent<PlayerMovement>(), rotatedDirection);

            yield return new WaitForSeconds(timeBetweenArrows);
        }
    }
}