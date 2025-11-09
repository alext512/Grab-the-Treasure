using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bars : MonoBehaviour {
    [SerializeField] float barOpenSpeed = 0f;
    [SerializeField] GameObject Lever;
    [SerializeField] float openTime = 1f;
    //Cache
    Rigidbody2D barRigidBody;
    

	// Use this for initialization
	void Start () {
        barRigidBody = GetComponent<Rigidbody2D>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenBars() {

        barRigidBody.linearVelocity = new Vector2(0f, barOpenSpeed);
        Invoke("StopBars", openTime);
    }
    public void StopBars() {
        barRigidBody.linearVelocity = new Vector2(0f, 0f);

    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    barRigidBody.velocity = new Vector2(0f, barOpenSpeed);
    //}
}
