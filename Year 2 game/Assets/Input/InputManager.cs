using UnityEngine;

public class InputManager : MonoBehaviour
{

    // Singleton for this class
    private static InputManager _instance;

    public static InputManager Instance {
        get {
            return _instance;
        }
    }

    private PlayerControls playerControls;

    private void Awake() {
        // If there is another instance of this script, destroy this instance
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        }
        else {
            // Assign this as an instance if one does not already exist
            _instance = this;
        }
        playerControls = new PlayerControls();
        
        // Placed here to avoid making another script
        // Makes the cursor invisible while playing the game
        Cursor.visible = false;
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void OnDisable() {
        playerControls.Disable();
    }

    /// <summary>
    /// Vector2 Composite values for movement
    /// </summary>
    /// <returns>Vector2, -1 if pressing left or down and 1 if pressing right or up</returns>
    public Vector2 GetPlayerMovement() {
        return playerControls.Player.Movement.ReadValue<Vector2>();
    }

    /// <summary>
    /// Difference in mouse position from the current and previous frame
    /// </summary>
    /// <returns>Vector2 direction of mouse movement</returns>
    public Vector2 GetMouseDelta() {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }

    /// <summary>
    /// Player has pressed jump button
    /// </summary>
    /// <returns>True if player jumped on this frame</returns>
    public bool PlayerJumpedThisFrame() {
        return playerControls.Player.Jump.triggered;
    }
}
