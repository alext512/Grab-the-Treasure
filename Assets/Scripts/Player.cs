using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour {
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float deathkick = 5f;
    [SerializeField] float waitAfterWin = 1f;
    [SerializeField] float maxDownwardsSpeed = 10f;
    [SerializeField] float timeTillFalling = 0.5f;
    [SerializeField] int jumpTime = 10;
    [SerializeField] bool startWithFlip = false;

    int score;


    Rigidbody2D myRigidBody;
    Animator myAnimator;
    public bool standStill = false;
    bool alive = true;
    bool win = false;
    bool turning = false;
    bool doubleJump = true;
    bool isJumping = false;
    public int jumpTimeCounter;
    public Vector2 platformSpeed = new Vector2(0f, 0f); // CHECK IF THIS IS USED
    //bool jumping = false;


    BoxCollider2D[] BoxColliders;
    AudioSource deathSound;
    AudioSource jumpSound;
    AudioSource doubleJumpSound;
    AudioSource landingSound;
    AudioSource[] allSounds;



    //BoxCollider2D flipCollider;
    //BoxCollider2D myFeet;

    // Use this for initialization
    void Start () {
        score = 0;
        if (startWithFlip) {
            IsFlipping();
        }
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();//InChildren<Animator>();

        BoxColliders = gameObject.GetComponents<BoxCollider2D>();
        allSounds = gameObject.GetComponents<AudioSource>();
        //deathSound = GetComponent<AudioSource>();
        deathSound = allSounds[0];
        jumpSound = allSounds[1];
        doubleJumpSound = allSounds[2];
        landingSound = allSounds[3];

        //flipCollider = GetComponent<BoxCollider2D>();
        //myFeet= GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
        //BackToMenu(); //developer's key (disabled)
        if (alive)
        {
            GotChest();
            Jump();
            if (!standStill && !win)
            {
                //jumpKeepPressed();
                WallJump();
                Run();
                Flip();
                //
                
                

                //WallJump();
            }
            else
            {
               // myRigidBody.velocity = new Vector2(0f, 0f);
                myAnimator.SetBool("Running", false);
 //               if (standStill) {
 //                   myRigidBody.velocity = platformSpeed;
//
 //               }
            }
            HazardKill();
        }

        if (myRigidBody.linearVelocity.y < -maxDownwardsSpeed) {
            //myRigidBody.velocity.y = maxDownwardsSpeed;
            myRigidBody.linearVelocity = new Vector2 (myRigidBody.linearVelocity.x, -maxDownwardsSpeed);// maxDownwardsSpeed);
        }
        //if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) ||
    //EventSystem.current.IsPointerOverGameObject()) {
          //  print("touch");
       // }

    }
    void FixedUpdate()//LateUpdate()
    {
        if (!standStill && !win)
        {
            jumpKeepPressed();
            //Flip();
            //Run();
        }
    }

    public void PlatformMovement()
    {
        if (standStill&& myRigidBody.linearVelocity != platformSpeed)
        {
            //myRigidBody.velocity = platformSpeed;

        }
    }

    private void Run() {
        //float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");

            Vector2 playerVelocity;

        if (IsFacingRight())
        {
            playerVelocity = new Vector2(runSpeed, myRigidBody.linearVelocity.y);
            
        }
        else {
            playerVelocity = new Vector2(-runSpeed, myRigidBody.linearVelocity.y);
        }
        playerVelocity = new Vector2(playerVelocity.x + platformSpeed.x, playerVelocity.y);
        //Vector2 playerVelocity = new Vector2(runSpeed, myRigidBody.velocity.y);

        myRigidBody.linearVelocity = playerVelocity;

        myAnimator.SetBool("Running", true);

    }

    private void Flip() {
        int allWalls = 1 << LayerMask.NameToLayer("Foreground") | 1 << LayerMask.NameToLayer("Moving Platform") | 1 << LayerMask.NameToLayer("Crumbling");
        if (((BoxColliders[0].IsTouchingLayers(allWalls)) && BoxColliders[1].IsTouchingLayers(allWalls)) && !turning)

            /*if ((BoxColliders[0].IsTouchingLayers(LayerMask.GetMask("Crumbling")) || BoxColliders[0].IsTouchingLayers(LayerMask.GetMask("Foreground"))||
            BoxColliders[0].IsTouchingLayers(LayerMask.GetMask("Moving Platform")))&&!turning
            &&(BoxColliders[1].IsTouchingLayers(LayerMask.GetMask("Foreground"))
            || BoxColliders[1].IsTouchingLayers(LayerMask.GetMask("Crumbling")) ||
            BoxColliders[1].IsTouchingLayers(LayerMask.GetMask("Moving Platform"))))*/
        {
//            print("flip");

            turning = true;
            myAnimator.SetBool("WallSlide", false);

            IsFlipping();
            Invoke("SwitchTurning", 0.01f);
        }
    }

    public void IsFlipping() {
        transform.localScale = new Vector2(-(Mathf.Sign(transform.localScale.x)), 1f);
    }
    private void SwitchTurning() {
        turning = false;
    }

    bool IsFacingRight() {
        return transform.localScale.x > 0;
    }





    public void Death() {
        if (alive) {
            alive = false;
            myAnimator.SetTrigger("Death");
            deathSound.Play();
            myRigidBody.linearVelocity = new Vector2(-myRigidBody.linearVelocity.x, deathkick);
            //Destroy(gameObject, 1f);
            Invoke("LoadSameLevel", 1);
        } 
    }

    public bool GotChest() {
        if (alive && !win) {
            if (myRigidBody.IsTouchingLayers(LayerMask.GetMask("Chest")))
            {
                win = true;
                myRigidBody.linearVelocity = new Vector2(0f, 0f);
                return true;

                //FindObjectOfType<GameSession>().ResetCoinsPicked();

                //StartCoroutine(LoadNextLevel());
            }
            else {
                return false;
            }
        }
        else
        {
            return false;

        }
    }

    IEnumerator LoadNextLevel()
    {

        yield return new WaitForSecondsRealtime(waitAfterWin);

        //TODO: make it better


        //FindObjectOfType<GameSession>().LoadNextLevel();

    }

    private void LoadSameLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        //FindObjectOfType<GameSession>().ProcessPlayerDeath();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



    private void HazardKill() {
        if(BoxColliders[1].IsTouchingLayers(LayerMask.GetMask("Hazards"))|| BoxColliders[2].IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {
                Death();
            }
    }

    private void BackToMenu() {
        if (Input.GetButtonDown("Fire2")) {
            SceneManager.LoadScene(0);
        }
   }


    //JUMPS


    private void SetJumpTrue()
    {
        myAnimator.SetTrigger("Jump"); //probably need to reset the trigger
        myAnimator.SetBool("JumpBool", true);
    }//for invoke

    private void jumpKeepPressed() {
        if ((CrossPlatformInputManager.GetButton("Jump") || (Input.GetMouseButton(0))) && isJumping == true) {
            if (jumpTimeCounter > 0)
            {
                
                myRigidBody.linearVelocity = new Vector2(myRigidBody.linearVelocity.x, jumpSpeed);
                jumpTimeCounter -= 1; //Time.deltaTime;

            }
            else {
                isJumping = false;
            }

        }
        else{// if (CrossPlatformInputManager.GetButtonUp("Jump") || Input.GetMouseButtonUp(0)){
            isJumping = false;

        }
    }

    public void PerformJump(bool doubleJump)
    {
        //   Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
        SetJumpTrue();
        myRigidBody.linearVelocity = new Vector2(myRigidBody.linearVelocity.x, jumpSpeed);
        isJumping = true;
        jumpTimeCounter = jumpTime;
        if (doubleJump)
        {
            doubleJumpSound.Play();
            myAnimator.SetTrigger("DoubleJump");
            //myAnimator.SetBool("DoubleJumpBool", true);

        }
        else if (!doubleJump)
        {
            jumpSound.Play();

            Invoke("SetJumpTrue", 0.02f);
            //myAnimator.SetTrigger("Jump");

        }
    }



    private void WallJump()
    {
        //int 
        int allWalls = 1 << LayerMask.NameToLayer("Foreground") | 1 << LayerMask.NameToLayer("Moving Platform") | 1 << LayerMask.NameToLayer("Crumbling");
        if (((BoxColliders[0].IsTouchingLayers(allWalls))&& !BoxColliders[1].IsTouchingLayers(allWalls))&&!turning)
            
            /*
            ||
    BoxColliders[0].IsTouchingLayers(LayerMask.GetMask("Moving Platform")) ||
    BoxColliders[0].IsTouchingLayers(LayerMask.GetMask("Crumbling"))) && !turning
    && (!BoxColliders[1].IsTouchingLayers(LayerMask.GetMask("Foreground"))
    || !BoxColliders[1].IsTouchingLayers(LayerMask.GetMask("Crumbling")) ||
    !BoxColliders[1].IsTouchingLayers(LayerMask.GetMask("Moving Platform")))))
    */
        {
            myAnimator.ResetTrigger("Jump");
            myAnimator.SetBool("JumpBool", false);

            myAnimator.SetBool("WallSlide", true);
            doubleJump = true;
            if (myRigidBody.linearVelocity.y < -(maxDownwardsSpeed) / 4)
            {
                //myRigidBody.velocity.y = maxDownwardsSpeed;
                myRigidBody.linearVelocity = new Vector2(myRigidBody.linearVelocity.x, -maxDownwardsSpeed / 4);// maxDownwardsSpeed);
            }

            if (Inputs.InputPressed())
            {
                IsFlipping();
                PerformJump(false);
                Invoke("TurnOffWallslide", 0.03f);
            }

        }
        else
        {
            Invoke("TurnOffWallslide", 0.03f);

            //myAnimator.SetBool("WallSlide", false);
        }


    }

    private void TurnOffWallslide() {
        myAnimator.SetBool("WallSlide", false);
    }

    private void Jump()
    {
        //if touching ground
        int allWalls = 1 << LayerMask.NameToLayer("Foreground") | 1 << LayerMask.NameToLayer("Moving Platform") | 1 << LayerMask.NameToLayer("Crumbling");
        if (BoxColliders[1].IsTouchingLayers(allWalls))
            /*if (BoxColliders[1].IsTouchingLayers(LayerMask.GetMask("Foreground"))
            || BoxColliders[1].IsTouchingLayers(LayerMask.GetMask("Crumbling")) ||
            BoxColliders[1].IsTouchingLayers(LayerMask.GetMask("Moving Platform")))*/
        {
            myAnimator.ResetTrigger("Jump");
            myAnimator.SetBool("JumpBool", false);
            if (myAnimator.GetBool("OnAir") == true)
            {
                landingSound.Play();
            }
            myAnimator.SetBool("OnAir", false);
            //myAnimator.SetBool("DoubleJump", false);
            //myAnimator.SetBool("JumpFalling", false);
            doubleJump = true;
            if (!standStill && !win)
            {
                if (Inputs.InputPressed())
                {
                    PerformJump(false);
                }
            }

        }
        else
        {
            //Invoke("SetOnAirTrue", 0.1f);
            myAnimator.SetBool("OnAir", true);

            if (doubleJump && !BoxColliders[0].IsTouchingLayers(allWalls))
            /*if (doubleJump && !((BoxColliders[0].IsTouchingLayers(LayerMask.GetMask("Foreground")) ||
    BoxColliders[0].IsTouchingLayers(LayerMask.GetMask("Moving Platform")) ||
    BoxColliders[0].IsTouchingLayers(LayerMask.GetMask("Crumbling")))))
    */
            {
                if (Inputs.InputPressed())
                {
                    PerformJump(true);
                    doubleJump = false;
                }
            }
        }
            }



    private void OnTriggerEnter2D(Collider2D collision)
    {



    }



}
