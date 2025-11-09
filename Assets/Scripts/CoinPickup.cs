using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour {

    [SerializeField] float pickupDelay = 5f;
    [SerializeField] int coinValue = 1;
    [SerializeField] GameObject generalHandler;
    //[SerializeField] AudioClip coinPickupSFX;

    AudioSource coinPickupSFX;


    bool triggered;
    Animator CoinAnimator;

    private void Start()
    {
        CoinAnimator = GetComponent<Animator>();
        coinPickupSFX = GetComponent<AudioSource>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(Pickup());

    }

    IEnumerator Pickup()
    {

        if (!triggered)
        {
            triggered = true;
            CoinAnimator.SetTrigger("Pickup");
            coinPickupSFX.Play();
            //AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
            //generalHandler.GetComponent<GameSession>().AddToCoins(coinValue);
            //FindObjectOfType<GameSession>().AddToCoins(coinValue);
            FindObjectOfType<Chest>().addScore(coinValue);
            yield return new WaitForSeconds(pickupDelay);
            Destroy(gameObject);

        }
        yield return null;

    }

    
}
