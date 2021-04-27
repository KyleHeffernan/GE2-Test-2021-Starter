using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    public GameObject ball;

    public float ballSpeed = 1.0f;

    public float lastThrow = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixedUpdate()
    {
        if(Input.GetButton("Space"))
        {
            //Checking the 1 Second throw cooldown
            if(lastThrow < Time.time - 1)
            {
                //Moves ball to player
                ball.transform.position = this.transform.position;
                Rigidbody rb = ball.GetComponent<Rigidbody>();
                //Resets velocity
                rb.velocity = Vector3.zero;
                //Adds a throwing force to ball
                rb.AddForce(transform.forward * ballSpeed);
                //Setting 1 second cooldown
                lastThrow = Time.time;
            }

        }
        



    }
}
