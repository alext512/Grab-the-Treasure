using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform lookAt;
    [SerializeField] bool smooth = true;
    [SerializeField] float smoothSpeed = 0.125f;
    [SerializeField] float offsetLimX = 0;
    [SerializeField] float offsetLimY = 0;
    [SerializeField] float toleranceXL = 0;
    [SerializeField] float toleranceYL = 0;
    [SerializeField] float toleranceXR = 0;
    [SerializeField] float toleranceYR = 0;
    [SerializeField] bool enable = true;

    Vector3 offset;
    Camera cam;

    float currentHeightPerc;
    float currentWidthPerc;
    float pixelRounding;

    void Start()
    {
        pixelRounding = 0.0625f;
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (lookAt == null || cam == null)
        {
            return;
        }

        currentHeightPerc = 0.5f;
        currentWidthPerc = 0.5f;

        float offsetX = Mathf.Clamp(-offsetLimX * (currentWidthPerc - 0.5f), -offsetLimX / 2f, offsetLimX / 2f);
        float offsetY = Mathf.Clamp(-offsetLimY * (currentHeightPerc - 0.5f), -offsetLimY / 2f, offsetLimY / 2f);

        offset = new Vector3(offsetX, offsetY, -10f);

        Vector3 playerPosition = lookAt.position + offset;
        Vector3 cameraPosition = transform.position;

        if (smooth)
        {
            Vector3 newPos = Vector3.Lerp(transform.position, playerPosition, smoothSpeed);
            transform.position = new Vector3(
                RoundToNearestPixel(newPos.x, cam, enable),
                RoundToNearestPixel(newPos.y, cam, enable),
                newPos.z);
            return;
        }

        float roundedX = Mathf.Round(playerPosition.x / pixelRounding) * pixelRounding;
        float roundedY = Mathf.Round(playerPosition.y / pixelRounding) * pixelRounding;
        cameraPosition = new Vector3(roundedX, roundedY, cameraPosition.z);
        transform.position = cameraPosition;
    }

    private float RoundToNearestPixel(float unityUnits, Camera viewingCamera, bool enablePixelRounding)
    {
        if (!enablePixelRounding)
        {
            return unityUnits;
        }

        float valueInPixels = (Screen.height / (viewingCamera.orthographicSize * 2f)) * unityUnits;
        valueInPixels = Mathf.Round(valueInPixels);
        return valueInPixels / (Screen.height / (viewingCamera.orthographicSize * 2f));
    }
}
