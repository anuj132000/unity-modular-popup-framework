using UnityEngine;

// A 2D collider that blocks touches and mouse input. Used mainly for blur/dim backgrounds behind popups.
[RequireComponent(typeof(Collider2D))]
[DisallowMultipleComponent]

public class TouchBlockerLayer : MonoBehaviour {

    private static int blockerMask = -1;

    private void Awake() {
        int touchBlockerLayer = LayerMask.NameToLayer("TouchBlocker");
        if (touchBlockerLayer == -1) {
            Debug.LogWarning("[TouchBlockerLayer] Layer 'TouchBlocker' not defined in project. Please create it in Tags & Layers.");
        } else if (gameObject.layer != touchBlockerLayer) {
            Debug.Log($"[TouchBlockerLayer] Forcing layer to 'TouchBlocker' on {gameObject.name}");
            gameObject.layer = touchBlockerLayer;
        }
        if (blockerMask == -1) {
            blockerMask = LayerMask.GetMask("TouchBlocker");
        }
    }

    /// Returns true if the given screen position is over a TouchBlocker collider.
    public static bool IsPointerOverBlocker(Vector3 screenPos) {
        if (blockerMask == -1) {
            blockerMask = LayerMask.GetMask("TouchBlocker");
        }
        if (blockerMask == 0) {
            return false; // layer not defined → do not block anything
        }
        Vector2 wp = Camera.main.ScreenToWorldPoint(screenPos);
        return Physics2D.OverlapPoint(wp, blockerMask) != null;
    }

    // Swallow OnMouse events so nothing behind responds
    private void OnMouseDown() {
        Debug.Log("TOUCH IS BLOCKED BY 'TouchBlockerLayer'...GameObject Name : " + gameObject.name);
        return;
    }
    private void OnMouseUp() { /* swallow */ }
    private void OnMouseDrag() { /* swallow */ }
    private void OnMouseOver() { /* swallow */ }
}




//using UnityEngine;

//public class TouchBlockerLayer : MonoBehaviour {

//    private void OnMouseDown() {
//        Debug.Log("TOUCH IS BLOCKED BY 'TouchBlockerLayer'...GameObject Name : " + gameObject.name);
//        return;
//    }
//}