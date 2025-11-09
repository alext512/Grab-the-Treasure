using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] float speed = 0.5f;
    [SerializeField] float verticalSpeed = 0f;
    [SerializeField] GameObject cameraObj;
    Vector2 startCameraVector;
    Vector2 currentCameraVector;
    Vector2 difference;
    Vector2 offset;
    float roundedX;
    float roundedY;
    float localScaleX;
    float localScaleY;
    float pixelValue;
    // Start is called before the first frame update
    void Start()
    {
        localScaleX = transform.localScale.x;
        localScaleY = transform.localScale.y;
        startCameraVector = cameraObj.transform.position;
        //print(LivesScore.pixelRounding);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        currentCameraVector = cameraObj.transform.position;
        difference = currentCameraVector - startCameraVector;
        roundedX= (float)Mathf.Round((difference.x * speed) / (LivesScore.pixelRounding/ localScaleX)) *(LivesScore.pixelRounding/ localScaleX);
        roundedY= (float)Mathf.Round((difference.y * verticalSpeed) / (LivesScore.pixelRounding / localScaleY)) * (LivesScore.pixelRounding / localScaleY);
        offset = new Vector2(roundedX, roundedY);
        //offset = new Vector2(((float)Mathf.Round((difference.x*speed)/ LivesScore.pixelRounding)* LivesScore.pixelRounding), 0);//Time.time * speed, 0);new Vector2((difference.x * speed) , 0); //
        //offset = new Vector2((difference.x * speed), 0);
        GetComponent<Renderer>().material.mainTextureOffset = offset;
        //Renderer.material.mainTextureOffset = offset;
    }
}
