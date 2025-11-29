using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public GameObject target;       // Le joueur
    public float smoothSpeed = 0.125f;
    public Vector3 offset;         // Décalage de la caméra (généralement Z = -10)

    public Vector2 minLimits;
    public Vector2 maxLimits;

    
    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minLimits.x, maxLimits.x);
        smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minLimits.y, maxLimits.y);
        smoothedPosition.z = offset.z;

        transform.position = smoothedPosition;
    }
}