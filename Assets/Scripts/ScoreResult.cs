using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreResult : MonoBehaviour {
    [SerializeField] Text resultText;
    // Use this for initialization
    void Start () {
        resultText.text = PlayerPrefsManager.GetResultScore().ToString();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
