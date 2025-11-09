using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PixelPerfectMovement : MonoBehaviour
{

    

    [SerializeField] int initialMaxCounter=1000;
    [SerializeField] int ppu = 16;
    [SerializeField] float speedX=1;
    [SerializeField] float speedY=1;
    public int inputX = 1;
    public int inputY = 1;

    int incrementX = 100;
    int incrementY = 100;
    int counterX;
    int counterY;
    Vector3 pixelMovementX;
    Vector3 pixelMovementY;
    Vector3 pixelMovementZ;


    int maxCounterX;
    int maxCounterY;

    GameObject player;

    public bool playerOn;


    void Start()
    {
        //inputX = 1;
        //inputY = 1;
        playerOn = false;
        counterX = 0 ;
        counterY = 0;
        float pixelSize = 1 / (float)ppu;
        //print(pixelSize);
        pixelMovementX = new Vector3(pixelSize, 0f, 0f);
        pixelMovementY = new Vector3(0f, pixelSize, 0f);
        pixelMovementZ = new Vector3(0f, 0f, pixelSize);
        maxCounterX = initialMaxCounter;
        maxCounterY = initialMaxCounter;

        SetSpeed(speedX, speedY);

        player = GameObject.FindGameObjectWithTag("player");
    }

    void Update()
    {
        //print(playerOn);
        //HandleInputX();
        //HandleInputY();

    }
    private void FixedUpdate()
    {
        
    
    HorizontalMovementFixed(inputX);
        VerticalMovementFixed(inputY);

    }

    private void SetSpeed(float speedX, float speedY)
    {
        incrementX = (int)(320 * speedX);
        incrementY = (int)(320 * speedY);

    }

   


    private void HorizontalMovementFixed(int direction)
    {
        if (direction == 0)
        {
            counterX = 0;
        }
        else
        {
            counterX += direction * incrementX;
        }
        while (counterX >= initialMaxCounter)
        {
            counterX -= maxCounterX;
            //print(transform.position);
            transform.position += pixelMovementX;
            //print(transform.position);
            if (playerOn)
            player.transform.position += pixelMovementX;
            //print(player.transform.position);
            //print(player.transform.position);
            //print("1");

        }
        while (counterX <= -initialMaxCounter)
        {
            counterX += maxCounterX;
            transform.position -= pixelMovementX;
            if(playerOn)
            player.transform.position -= pixelMovementX;
            //print("2");
        }

    }
    private void VerticalMovementFixed(int direction)
    {
        if (direction == 0)
        {
            counterY = 0;
        }
        else
        {
            counterY += direction * incrementY;
        }
        while (counterY >= initialMaxCounter)
        {
            counterY -= maxCounterY;
            transform.position += pixelMovementY;
            if (playerOn)
                player.transform.position += pixelMovementY;

        }
        while (counterY <= -initialMaxCounter)
        {
            counterY += maxCounterY;
            transform.position -= pixelMovementY;
            if (playerOn) 
                player.transform.position -= pixelMovementY;

        }

    }

    private void HorizontalMovement(int direction)
    {
        if (direction == 0)
        {
            counterX = 0;
            maxCounterX = initialMaxCounter;
        }
        if (direction < 0 && counterX > 0)
        {
            maxCounterX = initialMaxCounter;
        }
        else
        {
            counterX += direction * incrementX;
            if (maxCounterX > initialMaxCounter / 4)
            {
                maxCounterX = maxCounterX - 20;
            }
        }

        while (counterX >= maxCounterX)
        {
            counterX -= maxCounterX;
            transform.position += pixelMovementX;

        }
        while (counterX <= -maxCounterX)
        {
            counterX += maxCounterX;
            transform.position -= pixelMovementX;
        }

        print(counterX);
        print(transform.position);
    }

    private void HandleInputX()
    {

        if (Input.GetKey(KeyCode.RightArrow))
        {
            inputX = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            inputX = -1;
        }
        else
        {
            inputX = 0;
        }
    }
    private void HandleInputY()
    {

        if (Input.GetKey(KeyCode.UpArrow))
        {
            inputY = 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            inputY = -1;
        }
        else
        {
            inputY = 0;
        }
    }

}
