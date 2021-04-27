using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    public GameObject ball;

    public float ballSpeed = 1.0f;

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
            ball.transform.position = this.transform.position;
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.AddForce(transform.forward * ballSpeed);
        }
        



    }
}
