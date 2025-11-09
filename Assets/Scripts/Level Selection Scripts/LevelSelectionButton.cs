using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionButton : MonoBehaviour
{
    [SerializeField] bool isUp=true;
    [SerializeField] int arrowsBoundary = 0;
    [SerializeField] Camera lvlSelectionCamera;
    GameObject parent;
    float yPosition=0f;
    // Start is called before the first frame update
    void Start()
    {

        parent = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        moveToY();
    }

    private void OnMouseDown()
    {
        if (isUp)
        {
            print(yPosition);
            print(arrowsBoundary * 2 * lvlSelectionCamera.orthographicSize);
            if (yPosition < arrowsBoundary * 2*lvlSelectionCamera.orthographicSize)
            {
                print("test");
                yPosition += 2*lvlSelectionCamera.orthographicSize;
            }
        }
        else if (!isUp)
        {
            print("fjgjjgfhn");
            if (yPosition > arrowsBoundary * 2 * lvlSelectionCamera.orthographicSize)
            {
                yPosition -= 2 * lvlSelectionCamera.orthographicSize;
            }
        }
    }

    private void moveToY()
    {
        Vector2 parentPos = parent.transform.position;
        parent.transform.position = new Vector2(parentPos.x, yPosition);
    }
}
