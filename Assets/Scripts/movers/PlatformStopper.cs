using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformStopper : MonoBehaviour {
    [SerializeField] GameObject PlatformToStop;
    Rigidbody2D platformRigidBody;
    // Use this for initialization
    PixelPerfectMovement ppmScript;
    bool active;
    void Start () {
        active = true;
        gameObject.GetComponent<Renderer>().enabled = false;
        platformRigidBody = PlatformToStop.GetComponent<Rigidbody2D>();
        ppmScript = PlatformToStop.GetComponent<PixelPerfectMovement>();

    }
	
	// Update is called once per frame
	void Update () {
		if (!active)
        {
            StartCoroutine(ReEnableStopper());
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print("test");
        if (active)
        {
            active = false;
            ppmScript.inputX = -ppmScript.inputX;
            ppmScript.inputY = -ppmScript.inputY;
            //platformRigidBody.velocity = -platformRigidBody.velocity;
        }
    }

    IEnumerator ReEnableStopper()
    {
        yield return new WaitForSecondsRealtime(1);
        active = true;
    }
}
