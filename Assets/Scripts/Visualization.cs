using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Visualization : MonoBehaviour
{
    public int iterations;
    // public bool relativeToBody;
    // public CelestialBody centralBody;
    public Universe universe;
    private void Start() {
        universe = FindObjectOfType<Universe>();
        if (Application.isPlaying){
            HideOrbits();
        }
    }

    private void Update(){
        if (!Application.isPlaying) {
            DrawOrbits();
        }
    }

    private void DrawOrbits(){
        //Populate virtual bodies
        CelestialBody[] bodies = FindObjectsOfType<CelestialBody> ();
        VirtualBody[] virtualBodies = new VirtualBody[bodies.Length];

        for (int bodyIndex = 0; bodyIndex < bodies.Length; bodyIndex++){
            virtualBodies[bodyIndex] = new VirtualBody();
            virtualBodies[bodyIndex].position = bodies[bodyIndex].transform.position;
            virtualBodies[bodyIndex].velocity = bodies[bodyIndex].initialVelocity;
            // virtualBodies[bodyIndex].mass = bodies[bodyIndex].surfaceGravity * bodies[bodyIndex].radius * bodies[bodyIndex].radius * universe.gravitationalConstant;
            virtualBodies[bodyIndex].mass = bodies[bodyIndex].GetComponent<Rigidbody>().mass;
        }

        for (int i = 0; i < iterations; i ++){
             //Calculate virtual velocities
            for (int bodyIndex = 0; bodyIndex < virtualBodies.Length; bodyIndex++){
                for (int otherBodyIndex = 0; otherBodyIndex < virtualBodies.Length; otherBodyIndex++){
                    if (bodyIndex != otherBodyIndex){
                        var distance = Vector3.Distance(virtualBodies[bodyIndex].position, virtualBodies[otherBodyIndex].position);
                        var direction = (virtualBodies[otherBodyIndex].position - virtualBodies[bodyIndex].position).normalized;
                        var gravityForce = direction * universe.gravitationalConstant * (virtualBodies[bodyIndex].mass * virtualBodies[otherBodyIndex].mass)/ distance* distance;
                        var acceleration = gravityForce/virtualBodies[bodyIndex].mass;
                        virtualBodies[bodyIndex].velocity += acceleration;
                    }
                }
            }
            //Update the line
            for (int bodyIndex = 0; bodyIndex < bodies.Length; bodyIndex++){
                virtualBodies[bodyIndex].position += virtualBodies[bodyIndex].velocity;
                var lineRenderer = bodies[bodyIndex].gameObject.GetComponent<LineRenderer>();
                lineRenderer.positionCount = i+1;
                lineRenderer.SetPosition(i, virtualBodies[bodyIndex].position);
            }
        }
    }


    private void HideOrbits(){
        CelestialBody[] bodies = FindObjectsOfType<CelestialBody> ();

        // Draw paths
        for (int bodyIndex = 0; bodyIndex < bodies.Length; bodyIndex++) {
            var lineRenderer = bodies[bodyIndex].gameObject.GetComponentInChildren<LineRenderer> ();
            lineRenderer.positionCount = 0;
        }
    }

    private void UpdateRefVelocity(){
        
    }

    public void UpdateLine(){
        
    }

    class VirtualBody{
        public Vector3 position;
        public Vector3 velocity;
        public float mass;
    }


}
