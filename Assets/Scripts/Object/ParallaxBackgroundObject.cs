using UnityEngine;

public class ParallaxBackgroundObject : MonoBehaviour
{
    [SerializeField]
    private float parallaxMovementMultipler = .5f;
    [SerializeField]
    private Transform otherBackground;
    [SerializeField]
    private float limitX = 11f;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    private SpriteRenderer otherBackgroundSprite;
    private bool isAdjusting = false;
    private Vector3 deltaPosition;

    private void Awake()
    {
        PlayerManager.WorldPositionReset += WorldPositionReset;
    }
    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;

        if (otherBackground)
        {
            otherBackgroundSprite = otherBackground.GetComponent<SpriteRenderer>();
        }
    }
    private void OnDestroy()
    {
        PlayerManager.WorldPositionReset -= WorldPositionReset;
    }
    private void Update()
    {
        if (isAdjusting) return;
        Vector3 deltaPosition = cameraTransform.position - lastCameraPosition;
        deltaPosition.y = 0;
        deltaPosition.z = 0;
        if (deltaPosition.x == 0)
        {
            transform.position -= new Vector3(0.001f,0,0) * parallaxMovementMultipler * (1- parallaxMovementMultipler);
        }
        else
        {
            transform.position += deltaPosition * parallaxMovementMultipler;
            this.deltaPosition = deltaPosition;
        }
        if (transform.position.x <= lastCameraPosition.x - limitX)
        {
            if (otherBackgroundSprite)
            {
                transform.position = new Vector3(
                    otherBackground.position.x + (otherBackgroundSprite.size.x - 0.02f) * otherBackground.localScale.x,
                    transform.position.y);
            }
            else
            {
                transform.position = new Vector3(
                    lastCameraPosition.x + limitX,
                    transform.position.y);
            }
        }
        lastCameraPosition = cameraTransform.position;
    }

    private void WorldPositionReset()
    {
        isAdjusting = true;
        float relativeX = transform.position.x - PlayerManager.maxWorldX;
        transform.position = new Vector3(relativeX, transform.position.y);
        LeanTween.delayedCall(0.01f, () =>
        {
            lastCameraPosition = cameraTransform.position;
            isAdjusting = false;
        });
    }
}