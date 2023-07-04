using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    private CelestialBody[] allBodies;
    private Universe universe;

    public Rigidbody rb;
    private float mass;
    private Vector3 distance;
    private Vector3 acceleration;
    [SerializeField]
    private Vector3 initialVelocity;
    private Vector3 currentVelocity;
    private LineRenderer lineRenderer;
    private List<Vector3> linePoints;
    private Vector3 ref_pos;
    private Vector3 ref_velocity;
    private float orbitalVelocity;
    

  
    private void Awake() {
        universe = FindObjectOfType<Universe>();
        allBodies = FindObjectsOfType<CelestialBody>();
        rb = GetComponent<Rigidbody>();
        mass = rb.mass;

        
        ref_velocity = currentVelocity = initialVelocity;
        lineRenderer = GetComponent<LineRenderer>();
        linePoints = new List<Vector3>();
        ref_pos = rb.position;

    }

    public void UpdateVelocity(){   
        foreach (var body in allBodies){
            if (body != this){
                var distance = Vector3.Distance(transform.position, body.transform.position);
                var direction = (body.transform.position - this.transform.position).normalized;
                var gravityForce = direction * universe.gravitationalConstant * (mass * body.GetComponent<Rigidbody>().mass)/ distance* distance;
                var acceleration = gravityForce/mass;
                currentVelocity += acceleration;
            }
        }
    }

    public void UpdatePosition(){
        rb.position += currentVelocity;
    }


    public void UpdateRefVelocity(){   
        foreach (var body in allBodies){
            if (body != this){
                var distance = Vector3.Distance(ref_pos, body.ref_pos);
                var direction = (body.ref_pos - ref_pos).normalized;
                var gravityForce = direction * universe.gravitationalConstant * (mass * body.GetComponent<Rigidbody>().mass)/ distance* distance;
                var acceleration = gravityForce/mass;
                ref_velocity += acceleration;
            }
        }
    }
    public void UpdateLine(){
        ref_pos += ref_velocity;
        linePoints.Add(ref_pos);
        lineRenderer.positionCount = linePoints.Count;
        lineRenderer.SetPosition(linePoints.Count - 1, ref_pos);
    }

    
}
