using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailWagging : MonoBehaviour
{
    public float frequency = 1;
    public float amplitude = 40;
    public float theta = 0;
    // Start is called before the first frame update
    void Start()
    {
        theta = 2;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        theta = theta + Mathf.PI * 2.0f * Time.deltaTime * frequency * GetComponentInParent<Boid>().velocity.magnitude;
        float angle = Mathf.Sin(theta) * amplitude;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.right);
        transform.localRotation = q;
        
        
    }
}
