using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    private CelestialBody[] allBodies;
    public Rigidbody rb;
    public float mass;
    private Vector3 distance;
    private Vector3 acceleration;
    public Vector3 initialVelocity;
    private Vector3 currentVelocity;
    private LineRenderer lineRenderer;
    private List<Vector3> linePoints;
    private Vector3 ref_pos;
    private Vector3 ref_velocity;
    public float radius;
    public float surfaceGravity;
  
    private void Awake() {
        allBodies = FindObjectsOfType<CelestialBody>();
        rb = GetComponent<Rigidbody>();
        
        Debug.Log(name + radius);
        mass = CalculateMass();
        ref_velocity = currentVelocity = initialVelocity;
        lineRenderer = GetComponent<LineRenderer>();
        linePoints = new List<Vector3>();
        ref_pos = rb.position;
        transform.localScale = Vector3.one * radius;
    }

    public float CalculateMass(){
        return surfaceGravity * radius * radius / Universe.gravitationalConstant;
    }

    public void UpdateVelocity(){   
        foreach (var body in allBodies){
            if (body != this){
                var sqrDistance = (body.transform.position -transform.position).sqrMagnitude ;
                var direction = (body.transform.position - this.transform.position).normalized;
                var gravityForce = direction * Universe.gravitationalConstant * (mass * body.mass)/sqrDistance;
                var acceleration = gravityForce/mass;
                currentVelocity += acceleration * Universe.timeStep;
                // currentVelocity += acceleration;
            }
        }
    }

    public void UpdatePosition(){
        rb.position += currentVelocity * Universe.timeStep;
        // rb.position += currentVelocity;
    }


   

    
}
