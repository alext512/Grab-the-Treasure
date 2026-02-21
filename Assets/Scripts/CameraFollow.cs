using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField] Transform lookAt;
    [SerializeField] bool smooth = true;
    [SerializeField] float smoothSpeed = 0.125f;
    [SerializeField] float offsetLimX = 0;//Vector2 offset = new Vector2()
    [SerializeField] float offsetLimY = 0;
    [SerializeField] float toleranceXL = 0;
    [SerializeField] float toleranceYL = 0;
    [SerializeField] float toleranceXR = 0;
    [SerializeField] float toleranceYR = 0;
    [SerializeField] bool enable = true;
    //[SerializeField] GameObject botLeftCorner;
    //[SerializeField] GameObject topRightCorner;
    Vector3 offset;
    Camera cam;
    Vector2 botLeftPos;
    Vector2 topRightPos;
    float levelHeight;
    float levelWidth;

    float currentHeightPerc;
    float currentWidthPerc;

    float pixelRounding;

    // Use this for initialization
    void Start () {
        pixelRounding = 0.0625f;
        //botLeftPos = botLeftCorner.transform.position;
        //topRightPos = topRightCorner.transform.position;

        //levelHeight = topRightPos.y - botLeftPos.y;
        //levelWidth = topRightPos.x - botLeftPos.x;




        //offset = new Vector3(offsetX, offsetY, -10);
        //cam = gameObject.GetComponent<Camera>();
        //cam.aspect = 16f / 8f;
        //print(cam.orthographicSize);

    }
	
	// Update is called once per frame
	void LateUpdate () {
        //currentHeightPerc = (lookAt.transform.position.y - botLeftPos.y) / levelHeight;
        //currentWidthPerc = (lookAt.transform.position.x - botLeftPos.x) / levelWidth;
        currentHeightPerc = 0.5f;
        currentWidthPerc = 0.5f;

        float offsetX = Mathf.Clamp(-offsetLimX * (currentWidthPerc - 0.5f), -offsetLimX/2, offsetLimX/2);
        float offsetY = Mathf.Clamp (-offsetLimY * (currentHeightPerc - 0.5f), -offsetLimY/2, offsetLimY/2);

        offset = new Vector3(offsetX, offsetY, -10);//new Vector3((float)Mathf.Round((offsetX) / pixelRounding) * pixelRounding, (float)Mathf.Round((offsetY) / pixelRounding) * pixelRounding, -10);//offsetX, offsetY, -10);

        Vector3 playerPosition = lookAt.transform.position + offset;
        Vector3 cameraPosition = transform.position;

        if (smooth)
        {
            Vector3 newPos = Vector3.Lerp(transform.position, playerPosition, smoothSpeed);
            //print(newPos);
            print(newPos.x);
            //transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = new Vector3(RoundToNearestPixel(newPos.x, cam, enable), RoundToNearestPixel(newPos.y, cam, enable), newPos.z);
        }
        else
        { //tolerance: on flip, don't use this method immediately
                float roundedx = (float)Mathf.Round((playerPosition.x) / pixelRounding) * pixelRounding;
                cameraPosition = new Vector3(roundedx, cameraPosition.y, cameraPosition.z);
                float roundedy = (float)Mathf.Round((playerPosition.y) / pixelRounding) * pixelRounding;
                cameraPosition = new Vector3(cameraPosition.x, roundedy, cameraPosition.z);
            //ROUNDINGS TO NEARBY PIXEL ARE PERFORMED. REASON: TO AVOID BACKGROUND JITTERY MOTION
            //if (cameraPosition.x - playerPosition.x - toleranceXL > 0)
            //{
            //    //(float)Math.Round(number, MidpointRounding.AwayFromZero)
            //    //cameraPosition = new Vector3(playerPosition.x + toleranceXL, cameraPosition.y, cameraPosition.z);
            //    //cameraPosition = new Vector3(Math.Round(playerPosition.x + toleranceXL / 0.0625, cameraPosition.y, cameraPosition.z);
            //    //                print("1");
            //    float roundedx = (float)Mathf.Round((playerPosition.x + toleranceXL) / pixelRounding) * pixelRounding;
            //    cameraPosition = new Vector3(roundedx, cameraPosition.y, cameraPosition.z);
            //}
            //else if(cameraPosition.x - playerPosition.x + toleranceXL < 0)
            //{
            //    //cameraPosition = new Vector3(playerPosition.x - toleranceXL, cameraPosition.y, cameraPosition.z);
            //    float roundedx = (float)Mathf.Round((playerPosition.x - toleranceXL) / pixelRounding) * pixelRounding;
            //    cameraPosition = new Vector3(roundedx, cameraPosition.y, cameraPosition.z);

            //}
            ////transform.position = cameraPosition;

            //if (cameraPosition.y - playerPosition.y - toleranceYL > 0)
            //{
            //    //cameraPosition = new Vector3(cameraPosition.x, playerPosition.y + toleranceYL, cameraPosition.z);
            //    float roundedy = (float)Mathf.Round((playerPosition.y + toleranceYL) / pixelRounding) * pixelRounding;
            //    cameraPosition = new Vector3(cameraPosition.x, roundedy, cameraPosition.z);
            //    //               print("1");
            //}
            //else if (cameraPosition.y - playerPosition.y + toleranceYL < 0)
            //{
            //    //cameraPosition = new Vector3(cameraPosition.x, playerPosition.y - toleranceYL, cameraPosition.z);
            //    float roundedy = (float)Mathf.Round((playerPosition.y - toleranceYL) / pixelRounding) * pixelRounding;
            //    cameraPosition = new Vector3(cameraPosition.x, roundedy, cameraPosition.z);
            //}

            //transform.position = cameraPosition;

            //playerPosition = new Vector3(RoundToNearestPixel(playerPosition.x, cam, enable), RoundToNearestPixel(playerPosition.y, cam, enable), playerPosition.z);
            //transform.position = playerPosition;

            transform.position = cameraPosition;//new Vector3 (RoundToNearestPixel(cameraPosition.x, cam, enable), RoundToNearestPixel(cameraPosition.y, cam, enable), cameraPosition.z);
            //transform.position = desiredPosition;

        }
	}

    private float RoundToNearestPixel(float unityUnits, Camera viewingCamera, bool enable)
    {
        float valueInPixels = (Screen.height / (viewingCamera.orthographicSize * 2)) * unityUnits;
        if (enable)
        {
            
            valueInPixels = Mathf.Round(valueInPixels);
            float adjustedUnityUnits = valueInPixels / (Screen.height / (viewingCamera.orthographicSize * 2));
            return adjustedUnityUnits;
        }
        else
        {
            return unityUnits;
        }
    }
}
