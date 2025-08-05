using UnityEngine;

public class OrthographicCameraCropper : MonoBehaviour
{
    public Camera cam;               // Your orthographic camera
    public Transform targetObject;   // The object to tightly frame

    void Start()
    {
        CropToTarget();
    }

    void CropToTarget()
    {
        if (cam == null || targetObject == null)
        {
            Debug.LogWarning("Camera or target object not assigned.");
            return;
        }

        Renderer rend = targetObject.GetComponent<Renderer>();
        if (rend == null)
        {
            Debug.LogWarning("Target object has no Renderer.");
            return;
        }

        Bounds bounds = rend.bounds;
        float objectWidth = bounds.size.x;
        float objectHeight = bounds.size.z;

        // Get screen aspect ratio (width / height)
        float aspect = (float)Screen.width / Screen.height;

        // Set orthographic size to exactly match object's Z size (height), adjusted by aspect
        cam.orthographic = true;
        cam.orthographicSize = objectHeight / 2f;

        // Adjust size again if width is more limiting (to avoid showing outside)
        float cameraWidth = objectHeight * aspect;
        if (cameraWidth > objectWidth)
        {
            // Must reduce orthographic size to fit width instead
            cam.orthographicSize = (objectWidth / aspect) / 2f;
        }

        // Center the camera
        cam.transform.position = new Vector3(bounds.center.x, cam.transform.position.y, bounds.center.z);
    }
}


