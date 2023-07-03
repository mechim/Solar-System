using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    [SerializeField]
    private Earth earth;
    private Rigidbody rb;
    private float mass;
    private Vector3 distance;
    private Vector3 acceleration;
    private const float G = 0.00000000006674f;
    [SerializeField]
    private Vector3 initialVelocity;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        mass = rb.mass;

    }

    private void CalculateGravity(){    
        var distance = Vector3.Distance(transform.position, earth.transform.position);
        var gravityForce = G *( (mass * earth.GetComponent<Rigidbody>().mass))/ distance* distance;
        var direction = (this.transform.position - earth.transform.position).normalized;
        rb.AddForce(gravityForce*-direction*100 + initialVelocity);
        Debug.DrawLine(this.transform.position, earth.transform.position, Color.red, 1f);
    }
    private void Update() {
        CalculateGravity();
    }
}
