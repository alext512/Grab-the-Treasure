using UnityEngine;

public class SetScreenResolution : MonoBehaviour
{
    // Script for setting screen resolution.
    void Start()
    {
        Screen.SetResolution(1280, 720, false);
    }

}
