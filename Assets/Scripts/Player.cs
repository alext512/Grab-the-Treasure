using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float maxDownwardsSpeed = 10f;
    [SerializeField] int jumpTime = 10;
    [SerializeField] bool startWithFlip = false;

    [Header("Flow")]
    [SerializeField] float deathkick = 5f;
    [SerializeField] float waitAfterWin = 1f;
    [SerializeField] float timeTillFalling = 0.5f;

    // NOTE: Kept public fields for compatibility with existing scenes/prefabs.
    public bool standStill = false;
    public int jumpTimeCounter;
    public Vector2 platformSpeed = new Vector2(0f, 0f);

    Rigidbody2D myRigidBody;
    Animator myAnimator;

    bool alive = true;
    bool win = false;
    bool turning = false;
    bool doubleJump = true;
    bool isJumping = false;

    BoxCollider2D[] boxColliders;
    AudioSource deathSound;
    AudioSource jumpSound;
    AudioSource doubleJumpSound;
    AudioSource landingSound;

    int wallsMask;

    const string RunningParam = "Running";
    const string WallSlideParam = "WallSlide";
    const string JumpBoolParam = "JumpBool";
    const string OnAirParam = "OnAir";
    const string JumpTrigger = "Jump";
    const string DoubleJumpTrigger = "DoubleJump";
    const string DeathTrigger = "Death";

    void Start()
    {
        if (startWithFlip)
        {
            IsFlipping();
        }

        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        boxColliders = GetComponents<BoxCollider2D>();
        AudioSource[] allSounds = GetComponents<AudioSource>();

        // Existing prefab setup expects this ordering.
        deathSound = allSounds[0];
        jumpSound = allSounds[1];
        doubleJumpSound = allSounds[2];
        landingSound = allSounds[3];

        wallsMask = LayerMask.GetMask("Foreground", "Moving Platform", "Crumbling");
    }

    void Update()
    {
        if (alive)
        {
            GotChest();
            Jump();

            if (!standStill && !win)
            {
                WallJump();
                Run();
                Flip();
            }
            else
            {
                myAnimator.SetBool(RunningParam, false);
            }

            HazardKill();
        }

        ClampFallSpeed();
    }

    void FixedUpdate()
    {
        if (!standStill && !win)
        {
            jumpKeepPressed();
        }
    }

    private void ClampFallSpeed()
    {
        if (myRigidBody.linearVelocity.y < -maxDownwardsSpeed)
        {
            myRigidBody.linearVelocity = new Vector2(myRigidBody.linearVelocity.x, -maxDownwardsSpeed);
        }
    }

    public void PlatformMovement()
    {
        // Intentionally left as no-op: kept for scene references and future use.
    }

    private void Run()
    {
        Vector2 playerVelocity = IsFacingRight()
            ? new Vector2(runSpeed, myRigidBody.linearVelocity.y)
            : new Vector2(-runSpeed, myRigidBody.linearVelocity.y);

        playerVelocity = new Vector2(playerVelocity.x + platformSpeed.x, playerVelocity.y);
        myRigidBody.linearVelocity = playerVelocity;

        myAnimator.SetBool(RunningParam, true);
    }

    private void Flip()
    {
        if (IsTouchingWallFrontAndFeet() && !turning)
        {
            turning = true;
            myAnimator.SetBool(WallSlideParam, false);

            IsFlipping();
            Invoke(nameof(SwitchTurning), 0.01f);
        }
    }

    public void IsFlipping()
    {
        transform.localScale = new Vector2(-Mathf.Sign(transform.localScale.x), 1f);
    }

    private void SwitchTurning()
    {
        turning = false;
    }

    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    public void Death()
    {
        if (!alive)
        {
            return;
        }

        alive = false;
        myAnimator.SetTrigger(DeathTrigger);
        deathSound.Play();
        myRigidBody.linearVelocity = new Vector2(-myRigidBody.linearVelocity.x, deathkick);
        Invoke(nameof(LoadSameLevel), 1f);
    }

    public bool GotChest()
    {
        if (!alive || win)
        {
            return false;
        }

        if (!myRigidBody.IsTouchingLayers(LayerMask.GetMask("Chest")))
        {
            return false;
        }

        win = true;
        myRigidBody.linearVelocity = Vector2.zero;
        return true;
    }

    IEnumerator LoadNextLevel()
    {
        // Kept for compatibility with potential animation/event hooks.
        yield return new WaitForSecondsRealtime(waitAfterWin);
    }

    private void LoadSameLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void HazardKill()
    {
        if (boxColliders[1].IsTouchingLayers(LayerMask.GetMask("Hazards")) ||
            boxColliders[2].IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {
            Death();
        }
    }

    private void BackToMenu()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void SetJumpTrue()
    {
        myAnimator.SetTrigger(JumpTrigger);
        myAnimator.SetBool(JumpBoolParam, true);
    }

    private void jumpKeepPressed()
    {
        bool jumpHeld = CrossPlatformInputManager.GetButton("Jump") || Input.GetMouseButton(0);

        if (jumpHeld && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                myRigidBody.linearVelocity = new Vector2(myRigidBody.linearVelocity.x, jumpSpeed);
                jumpTimeCounter -= 1;
            }
            else
            {
                isJumping = false;
            }
        }
        else
        {
            isJumping = false;
        }
    }

    public void PerformJump(bool isDoubleJump)
    {
        SetJumpTrue();
        myRigidBody.linearVelocity = new Vector2(myRigidBody.linearVelocity.x, jumpSpeed);
        isJumping = true;
        jumpTimeCounter = jumpTime;

        if (isDoubleJump)
        {
            doubleJumpSound.Play();
            myAnimator.SetTrigger(DoubleJumpTrigger);
        }
        else
        {
            jumpSound.Play();
            Invoke(nameof(SetJumpTrue), 0.02f);
        }
    }

    private void WallJump()
    {
        bool touchingWall = boxColliders[0].IsTouchingLayers(wallsMask);
        bool touchingGround = boxColliders[1].IsTouchingLayers(wallsMask);

        if (touchingWall && !touchingGround && !turning)
        {
            myAnimator.ResetTrigger(JumpTrigger);
            myAnimator.SetBool(JumpBoolParam, false);

            myAnimator.SetBool(WallSlideParam, true);
            doubleJump = true;

            if (myRigidBody.linearVelocity.y < -(maxDownwardsSpeed / 4f))
            {
                myRigidBody.linearVelocity = new Vector2(myRigidBody.linearVelocity.x, -maxDownwardsSpeed / 4f);
            }

            if (Inputs.InputPressed())
            {
                IsFlipping();
                PerformJump(false);
                Invoke(nameof(TurnOffWallslide), 0.03f);
            }
        }
        else
        {
            Invoke(nameof(TurnOffWallslide), 0.03f);
        }
    }

    private void TurnOffWallslide()
    {
        myAnimator.SetBool(WallSlideParam, false);
    }

    private void Jump()
    {
        if (boxColliders[1].IsTouchingLayers(wallsMask))
        {
            myAnimator.ResetTrigger(JumpTrigger);
            myAnimator.SetBool(JumpBoolParam, false);

            if (myAnimator.GetBool(OnAirParam))
            {
                landingSound.Play();
            }

            myAnimator.SetBool(OnAirParam, false);
            doubleJump = true;

            if (!standStill && !win && Inputs.InputPressed())
            {
                PerformJump(false);
            }

            return;
        }

        myAnimator.SetBool(OnAirParam, true);

        if (doubleJump && !boxColliders[0].IsTouchingLayers(wallsMask) && Inputs.InputPressed())
        {
            PerformJump(true);
            doubleJump = false;
        }
    }

    private bool IsTouchingWallFrontAndFeet()
    {
        return boxColliders[0].IsTouchingLayers(wallsMask) && boxColliders[1].IsTouchingLayers(wallsMask);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
