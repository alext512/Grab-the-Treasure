using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vamtemp : MonoBehaviour
{
    // Start is called before the first frame update
    Camera cam;
    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        cam.aspect = 16f / 10f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
