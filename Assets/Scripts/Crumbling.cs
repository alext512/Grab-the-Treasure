using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Crumbling : MonoBehaviour {
    [SerializeField] float colliderDestroyDelay;

    Animator crumblingAnimator;
    BoxCollider2D[] BoxColliders;

    // Use this for initialization


    bool triggered;
    void Start () {
        BoxColliders = gameObject.GetComponents<BoxCollider2D>();
        crumblingAnimator = GetComponent<Animator>();
        triggered = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator CrumbleBlock() {

        if (!triggered)
        {
            triggered = true;
            crumblingAnimator.SetTrigger("Crumbling");

            yield return new WaitForSeconds(colliderDestroyDelay);
            BoxColliders[0].enabled = false;
            BoxColliders[1].enabled = false;
            Destroy(gameObject);
        }
        yield return null;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        StartCoroutine(CrumbleBlock());
    }
}


