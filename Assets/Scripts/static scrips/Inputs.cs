using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.EventSystems;

public class Inputs : MonoBehaviour
{
    /// <summary>
    /// Centralized jump intent check for keyboard, controller and touch/mouse.
    /// UI clicks/touches are ignored so gameplay input does not leak through menus.
    /// </summary>
    public static bool InputPressed()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            return true;
        }

        if (!Input.GetMouseButtonDown(0))
        {
            return false;
        }

        // For touch devices, only accept a fresh touch that is not over a selected UI object.
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            return EventSystem.current.currentSelectedGameObject == null;
        }

        // Mouse fallback path.
        return EventSystem.current.currentSelectedGameObject == null;
    }
}
