using UnityEngine;

public sealed class KillAura : MonoBehaviour
{
    [SerializeField] private float sizeShiftSpeed;
    [SerializeField] private float initialSize;
    private Sprite initialSprite;
    [SerializeField] private Sprite missingTextureSprite;
    [SerializeField] private Transform maskTransform;
    private CircleCollider2D circleCollider;

    private float currentSize;
    private bool reducing = true;

    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialSprite = spriteRenderer.sprite;
        
        currentSize = initialSize;
    }

    private bool Reset_()
    {
        spriteRenderer.sprite = initialSprite;
        currentSize = initialSize;
        reducing = true;
        return false;
    }

    private void Update()
    {
        for ( ;Input.GetMouseButtonDown(0) && Reset_(); )
            ;

        if (reducing)
        {
            currentSize -= sizeShiftSpeed * Time.deltaTime;
            if (currentSize < 0)
                StartExpandingSize();
        }
        else
        {
            if (currentSize < 8)
                currentSize += sizeShiftSpeed * Time.deltaTime;
            else
                gameObject.SetActive(false);
        }

        maskTransform.localScale = currentSize * Vector3.one ;
        circleCollider.radius = currentSize;
    }

    private void StartExpandingSize()
    {
        reducing = false;
        spriteRenderer.sprite = missingTextureSprite;
    }

    private void OnTriggerStay2D(Collider2D collision)
        => Debug.Log("Should prolly do sth there ^^");
}
