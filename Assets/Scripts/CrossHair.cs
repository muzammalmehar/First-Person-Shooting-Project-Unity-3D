using UnityEngine;

public class CrossHair : MonoBehaviour
{
    void Update()
    {
        // Get the mouse position in screen coordinates
        Vector3 mousePosition = Input.mousePosition;

        // Convert screen coordinates to world coordinates
        mousePosition.z = 10f; // Distance from the camera
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Update the position of the crosshair
        transform.position = worldPosition;
    }
}
