using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionCameraMovement : MonoBehaviour
{
    [SerializeField] int topY;
    [SerializeField] int botY;
    [SerializeField] GameObject upArrow;
    [SerializeField] GameObject downArrow;
    [SerializeField] int speedMaxMovementSegment =10;
    float yPosition=0;
    Vector2 currentPosition;
    Vector2 movingPosition;
    int upButtonLayerMask;// = 1 << LayerMask.NameToLayer("UpButton");
    int downButtonLayerMask;// = 1 << LayerMask.NameToLayer("downButton");

    int movementSegment;

    bool isMoving;


    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
        movementSegment = 0;
        upButtonLayerMask = 1 << LayerMask.NameToLayer("UpButton");
        downButtonLayerMask = 1 << LayerMask.NameToLayer("DownButton");
        hideOrShowButtons();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("clicked");
            RaycastHit2D hitUp = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.up, .1f, upButtonLayerMask);
            RaycastHit2D hitDown = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.up, .1f, downButtonLayerMask);

            if (hitUp.collider != null)
            {
                print("UpClicked");
                tryToMoveUp();
            }
            if (hitDown.collider != null)
            {
                print("DownClicked");

                tryToMoveDown();
            }
        }

    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            movementSegment = movementSegment + 1;
            transform.position = Vector2.Lerp(currentPosition, movingPosition, ((float)movementSegment) / speedMaxMovementSegment);
            if(movementSegment== speedMaxMovementSegment)
            {
                movementSegment = 0;
                isMoving = false;
            }
        }
    }


    void tryToMoveUp()
    {
        if (yPosition < topY * 2 * gameObject.GetComponent<Camera>().orthographicSize)
        {
            yPosition += 2 * gameObject.GetComponent<Camera>().orthographicSize;
            //transform.position = new Vector2(transform.position.x, yPosition); //change to lerp
            hideOrShowButtons();
            isMoving = true;
            movementSegment = 0;
            currentPosition = transform.position;
            movingPosition = new Vector2(transform.position.x, yPosition);
        }
    }

    void tryToMoveDown()
    {
        if (yPosition > botY * 2 * gameObject.GetComponent<Camera>().orthographicSize)
        {
            yPosition -= 2 * gameObject.GetComponent<Camera>().orthographicSize;
            //transform.position = new Vector2(transform.position.x, yPosition);
            hideOrShowButtons();
            isMoving = true;
            movementSegment = 0;
            currentPosition = transform.position;
            movingPosition = new Vector2(transform.position.x, yPosition);
        }
    }
    
    void hideOrShowButtons()
    {
        if (yPosition < topY * 2 * gameObject.GetComponent<Camera>().orthographicSize)
        {
            //show up
            upArrow.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            //hide
            upArrow.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (yPosition > botY * 2 * gameObject.GetComponent<Camera>().orthographicSize)
        {
            //show down
            downArrow.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            //hide
            downArrow.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

}
