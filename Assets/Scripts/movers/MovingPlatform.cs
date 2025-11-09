using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField] float platformVerticalSpeed = 1f;
    [SerializeField] float platformHorizontalSpeed = 0f;
    //[SerializeField] GameObject player;
    //[SerializeField] GameObject thisPlatform;
    Rigidbody2D MovingPlatformRigidBody;
    bool playerOn = false;
    GameObject player;
    public float direction = 1f;
    PixelPerfectMovement ppmScript;





    // Use this for initialization
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("player");
        MovingPlatformRigidBody = GetComponent<Rigidbody2D>();
        //MovingPlatformRigidBody.velocity = new Vector2(platformVerticalSpeed, platformHorizontalSpeed);
        ppmScript = GetComponent<PixelPerfectMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        //Vector2 currentVelocity = MovingPlatformRigidBody.velocity;
    }

    private void FixedUpdate()
    {
        

        
    }

    private void HorizontalMovementFixed(int direction)
    {
    }

    private void movePlayerAlong() {
        Vector2 CurrentPlayerV = player.GetComponent<Rigidbody2D>().velocity;
        Vector2 CurrentPlatformV = MovingPlatformRigidBody.velocity;


        
        player.GetComponent<Player>().platformSpeed = CurrentPlatformV;
        player.GetComponent<Player>().PlatformMovement();
        //player.GetComponent<Rigidbody2D>().velocity =
        //CurrentPlayerV + CurrentPlatformV;
        //new Vector2 (MovingPlatformRigidBody.velocity.x, player.GetComponent<Rigidbody2D>().velocity.y);
    }

/*
    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.tag == "player")
        {
            if (!playerOn)
            {

                playerOn = true;
                ppmScript.playerOn = true;
            }
            //movePlayerAlong();

        }
    }
    */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print("test");

        if (collision.gameObject.tag == "player")
        {
            if (!playerOn)
            {

                playerOn = true;
                ppmScript.playerOn = true;
            }
            //movePlayerAlong();

        }
    }
    private void OnTriggerExit2D(Collider2D collider) //(Collision2D collision)
    {
        if (collider.gameObject.tag == "player") {
            playerOn = false;
            ppmScript.playerOn = false;
            player.GetComponent<Player>().platformSpeed = new Vector2(0f, 0f);
        }
    }
}



