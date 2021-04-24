using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTools : MonoBehaviour
{
    Camera _camera;

    private void Start() { _camera = GetComponent<Camera>(); }

    public Vector3 ClampToScreenBounds(Vector3 worldPositionIn) {

        // Clamp world position to viewport limits
        Vector3 viewportPosition = _camera.WorldToViewportPoint(worldPositionIn);
        viewportPosition = new Vector3(
            Mathf.Clamp(viewportPosition.x, 0f, 1f),
            Mathf.Clamp(viewportPosition.y, 0f, 1f),
            viewportPosition.z);

        // Return clamped position in world space
        Vector3 worldPositionOut = _camera.ViewportToWorldPoint(viewportPosition);
        return worldPositionOut;
    }

}
