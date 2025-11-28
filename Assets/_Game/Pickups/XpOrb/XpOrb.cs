using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class XpOrb : BasePickupable
{
    [field: SerializeField] public int XpValue { get; private set; }

    [SerializeField] private bool isSuper = false;
    private CircleCollider2D collider2d = null;

    private void Start()
    {
        collider2d = GetComponent<CircleCollider2D>();

        if (isSuper)
        {
            collider2d.isTrigger = false;
            StartCoroutine(TimeToRoll(2));
        }
        else
            collider2d.isTrigger = true;
    }

    protected sealed override void OnPickup(GameObject gameObject_)
    {
        if (gameObject_.TryGetComponent<PlayerXP>(out var playerXP))
        {
            playerXP.EarnXP(XpValue);
            Destroy(gameObject);
        }
    }

    private IEnumerator TimeToRoll(float deltaTime)
    {
        yield return new WaitForSeconds(deltaTime);

        collider2d.isTrigger = true;

        List<Collider2D> results = new List<Collider2D>();
        collider2d.Overlap(results);

        foreach (var obj in results)
        {
            if (obj.gameObject.TryGetComponent<PlayerXP>(out var playerXP))
            {
                OnPickup(obj.gameObject);
                yield break;
            }
        }
    }
}