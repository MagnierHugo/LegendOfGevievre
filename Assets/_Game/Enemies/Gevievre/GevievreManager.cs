using UnityEngine;

public class GevievreManager : MonoBehaviour
{
    [SerializeField] private GameObject gevievrePrefab;
    [SerializeField] private GameObject parentPrefab;
    [SerializeField] private float chanceToSpawn = 0.05f;
    [SerializeField] private int groupeRadius = 5;
    [SerializeField] private int numberOfGevievre = 20;

    [SerializeField] private Vector2 minBounds = new Vector2(-32f, -32f);
    [SerializeField] private Vector2 maxBounds = new Vector2(32f, 32f);

    private void Update()
    {
        if (GameManager.GamePaused)
            return;

        if (Random.Range(0f, 1f) > chanceToSpawn)
            return;

        Vector2 position = new Vector2(Random.Range(minBounds.x, maxBounds.x), Random.Range(minBounds.y, maxBounds.y));
        GameObject parent = Instantiate(parentPrefab, position, Quaternion.identity);

        for (int i = 0; i < numberOfGevievre; i++)
        {
            position.x = Random.Range(0f, groupeRadius);
            position.y = Random.Range(0f, groupeRadius);
            Instantiate(gevievrePrefab, Vector3.zero, Quaternion.identity, parent.transform).transform.localPosition = position;
        }
    }
}
