using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingCamera : MonoBehaviour
{
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        cam.aspect = 4f / 3f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
