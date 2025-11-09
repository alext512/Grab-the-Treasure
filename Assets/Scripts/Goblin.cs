using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour {
    [SerializeField] float goblinSpeed = 2f;
    [SerializeField] GameObject player;
    [SerializeField] bool facingRight = true;

    PolygonCollider2D deadCollider;
    BoxCollider2D goblinCollider;
    Rigidbody2D goblinRigidBody;
    CircleCollider2D noseCollider;
    Animator goblinAnimator;
    bool alive;
    bool killable;
    

	// Use this for initialization
	void Start () {
        goblinAnimator = GetComponent<Animator>();
        deadCollider = GetComponent<PolygonCollider2D>();
        goblinCollider = GetComponent<BoxCollider2D>();
        noseCollider = GetComponent<CircleCollider2D>();
        goblinRigidBody = GetComponent<Rigidbody2D>();
        alive = true;
        killable = true;
        if (!facingRight)
        {
            transform.localScale = new Vector2(-(Mathf.Sign(goblinRigidBody.linearVelocity.x)), 1f);
        }

    }
	
	// Update is called once per frame
	void Update () {
        Death();
        if (alive)
        {
            Run();
            Flip();
            Kill();
        }


    }

    private void Death()
    {
        if (deadCollider.IsTouchingLayers(LayerMask.GetMask("Player")) && alive && killable) {
            alive = false;
            goblinAnimator.SetTrigger("Dead");
            goblinRigidBody.isKinematic = false;
            Destroy(goblinCollider);
            Destroy(gameObject, 1f);

            goblinRigidBody.linearVelocity = new Vector2(goblinRigidBody.linearVelocity.x, 5f);

        }
    }

    private void Run() {
        Vector2 goblinVelocity;
        if (IsFacingRight()) {
            goblinVelocity = new Vector2(goblinSpeed, 0f);

        }
        else
        {
            goblinVelocity = new Vector2(-goblinSpeed, 0f);
        }
        goblinRigidBody.linearVelocity = goblinVelocity;
    }

    private void Flip()
    {
        if (noseCollider.IsTouchingLayers(LayerMask.GetMask("Foreground")))
        {
            transform.localScale = new Vector2(-(Mathf.Sign(goblinRigidBody.linearVelocity.x)), 1f);
        }
    }

    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void Kill() {
        if (goblinCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            player.GetComponent<Player>().Death();
            killable = false;

        }
    }


}
