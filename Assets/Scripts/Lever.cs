using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour {


    [SerializeField] GameObject LeverOn;
    [SerializeField] GameObject LeverOff;
    [SerializeField] GameObject Bars;

    public bool isOn = false;

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = LeverOff.GetComponent<SpriteRenderer>().sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isOn == false) {

        gameObject.GetComponent<SpriteRenderer>().sprite = LeverOn.GetComponent<SpriteRenderer>().sprite;
        isOn = true;
        Bars.GetComponent<Bars>().OpenBars();
    }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
