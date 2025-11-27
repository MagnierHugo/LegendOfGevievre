using UnityEngine;

public class BasePickable : MonoBehaviour
{
    protected virtual int IsPickedUp()
    {
        return 0;
    }
}