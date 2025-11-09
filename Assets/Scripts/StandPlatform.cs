using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class StandPlatform : MonoBehaviour {
    //[SerializeField] GameObject player;
    [SerializeField] bool jumpPlatform=true;
    //[SerializeField] GameObject platform;
    [SerializeField] bool causeFlip=false;

    BoxCollider2D sPlatformCol;
    bool active;
    bool standStillLocal;
    bool flipped;
    //bool applied;
    GameObject player;
    

	// Use this for initialization
	void Start () {
        sPlatformCol = GetComponent<BoxCollider2D>();
        active = true;
        standStillLocal = false;
        flipped = false;
        //applied = false;
        player = GameObject.FindGameObjectWithTag("player");

    }

    // Update is called once per frame

    void Update () {
        if (active)
        {
            StartCoroutine(StandStill());
        }
        if (standStillLocal) {

            //player.GetComponent<Rigidbody2D>().velocity= GetComponent<Rigidbody2D>().velocity;
        }

    }

    private void FlipPlayer() {
        if (causeFlip && !flipped)
        {
            player.GetComponent<Player>().IsFlipping();
            flipped = true;

        }
    }

    private void continueRunning() {
        player.GetComponent<Player>().standStill = false;
        standStillLocal = false;

        if (jumpPlatform) {
            
            player.GetComponent<Player>().PerformJump(false);

        }
    }


    IEnumerator StandStill() {
        if (sPlatformCol.IsTouchingLayers(LayerMask.GetMask("Player"))
    && !standStillLocal)// && !applied)
        {
            //applied = true;
            //print("touched");
            player.GetComponent<Player>().standStill = true;
            player.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0f, 0f);

            standStillLocal = true;
            FlipPlayer();
            yield return null;
        }
        else if (standStillLocal)
        {
            if (Inputs.InputPressed())//static script
            {

                Invoke("continueRunning", 0.01f);


                //this.gameObject.GetComponent<StandPlatform>().enabled = false;
                active = false;
                yield return new WaitForSeconds(1);
                active = true;
                //applied = false;
                //this.gameObject.GetComponent<StandPlatform>().enabled = true;

            }
        }
    }
}
