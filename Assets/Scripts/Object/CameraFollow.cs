using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float offSetX = 0.5f;

    [SerializeField]
    private Transform cameraTransform;

    private void Update()
    {
        cameraTransform.position = new Vector3(target.position.x - offSetX, 0, -10);
    }
}