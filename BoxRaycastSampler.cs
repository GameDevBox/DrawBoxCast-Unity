/*
 * BoxRaycastSampler.cs
 * Created by Arian - GameDevBox
 * YouTube Channel: https://www.youtube.com/@GameDevBox
 *
 * 🎮 Want more Unity tips, tools, and advanced systems?
 * 🧠 Learn from practical examples and well-explained logic.
 * 📦 Subscribe to GameDevBox for more game dev content!
 *
 * This script performs a BoxCast from the camera and visualizes the cast 
 * in the Scene and Game view using Debug.DrawLine and Gizmos.
 */

using UnityEngine;

public class BoxRaycastSampler : MonoBehaviour
{
    // Reference to the camera from which the BoxCast will be made
    public Camera cam;

    // Size of the box used in the cast (width, height, depth)
    public Vector3 boxSize = new Vector3(1, 1, 1);

    // How far the box will cast from the origin point
    public float castDistance = 5f;

    // Layers that the cast should interact with
    public LayerMask hitLayers;

    // Color for visualizing when something is hit
    public Color hitColor = Color.red;

    // Color for visualizing when no hit is detected
    public Color noHitColor = Color.green;

    private RaycastHit hitInfo; // Holds info about what was hit
    private bool hasHit;        // Did the BoxCast hit something?

    private void Update()
    {
        // Automatically find the main camera if not set
        if (cam == null)
        {
            cam = Camera.main;
            if (cam == null) return; // Exit if no camera found
        }

        // Origin and direction based on camera
        Vector3 origin = cam.transform.position;
        Vector3 direction = cam.transform.forward;
        Quaternion rotation = cam.transform.rotation;

        // Perform the BoxCast
        hasHit = Physics.BoxCast(origin, boxSize / 2, direction, out hitInfo, rotation, castDistance, hitLayers);

        // If a hit occurred, log it
        if (hasHit && hitInfo.collider != null)
        {
            Debug.Log($"Hit: {hitInfo.collider.name}", hitInfo.collider.gameObject);
        }

        // -------------------SAMPLE DRAWING-------------------

        //DebugBoxCast.SimpleDrawBox(origin, boxSize / 2, rotation, Color.yellow);

        //DebugBoxCast.SimpleDrawBoxCast(origin, boxSize / 2, rotation, direction, castDistance, Color.cyan);

        DebugBoxCast.AdvancedDrawBoxCast(origin, boxSize / 2, rotation, direction, castDistance,
            hasHit, hitDistance: hasHit ? hitInfo.distance : 0, hitColor, noHitColor);

        //DebugBoxCast.DrawBoxCastOnHit(origin, boxSize / 2, rotation, direction, hitInfo.distance, Color.purple);
    }
}
