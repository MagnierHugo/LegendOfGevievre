using UnityEngine;

public class MagnetPickUp : BasePickupable
{
    protected sealed override void OnPickup(GameObject gameObject_)
    {
        PlayerMagnet playerMagnet = gameObject_.GetComponentInChildren<PlayerMagnet>();
        if (playerMagnet != null)
        {
            playerMagnet.IncreaseRange(1000);
            Destroy(gameObject);
        }
    }
}
