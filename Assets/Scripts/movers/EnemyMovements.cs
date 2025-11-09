using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovements : MonoBehaviour
{
    [SerializeField] float startAngle;
    [SerializeField] float width;
    [SerializeField] float height;
    [SerializeField] float speed;

    float x;
    float y;
    float z;
    float counter;
    // Start is called before the first frame update
    void Start()
    {
        counter = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        CircleMovement();
    }

    private void CircleMovement() {
        counter = counter + 0.01f;
        x = Mathf.Cos(counter);
        y = Mathf.Sin(counter);
        z = 0;
        transform.position = new Vector3(x, y, z);
    }
}
