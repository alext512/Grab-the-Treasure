using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.EventSystems;

public class Inputs : MonoBehaviour {

    public static bool InputPressed() {
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            return true;
            
        }
        else if (Input.GetMouseButtonDown(0))// && Input.mousePosition.x > Screen.width / 2))
        {

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (EventSystem.current.currentSelectedGameObject == null) //!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) && 
                {

                    return true;
                }
                else
                {

                    return false;
                }
            }
            else if (EventSystem.current.currentSelectedGameObject == null) //!EventSystem.current.IsPointerOverGameObject() && 
            {

                return true;
            }
            else
            {

                return false;
            }
        }
        else {

            return false;
        }
        }
    }
