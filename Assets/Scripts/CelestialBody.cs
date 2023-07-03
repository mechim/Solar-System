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
    public bool isVisualizing;
    private LineRenderer lineRenderer;
    private List<Vector3> lienPoints;
    private Vector3 ref_pos;
    private float orbitalVelocity;
    

  
    private void Awake() {
        universe = FindObjectOfType<Universe>();
        allBodies = FindObjectsOfType<CelestialBody>();
        rb = GetComponent<Rigidbody>();
        mass = rb.mass;

        
        currentVelocity = initialVelocity;
        lineRenderer = GetComponent<LineRenderer>();
        lienPoints = new List<Vector3>();
        ref_pos = rb.position;

    }

    public void UpdateVelocity(){   
        foreach (var body in allBodies){
            if (body != this){
                var distance = Vector3.Distance(transform.position, body.transform.position);
                
                var direction = (-this.transform.position + body.transform.position).normalized;
                var gravityForce = direction * universe.gravitationalConstant *( (mass * body.GetComponent<Rigidbody>().mass))/ distance* distance;
                var acceleration = gravityForce/mass;
                currentVelocity += acceleration;
            }
        }
    }

    public void UpdatePosition(){
        rb.position += currentVelocity;
    }
    
    public void UpdateLine(){
        ref_pos += currentVelocity;
        lienPoints.Add(ref_pos);
        lineRenderer.positionCount = lienPoints.Count;
        lineRenderer.SetPosition(lienPoints.Count -1, ref_pos);
    }
}
