using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySimulation : MonoBehaviour
{
   CelestialBody[] bodies;

   private void Awake() {
        bodies = FindObjectsOfType<CelestialBody>();
   }

    private void FixedUpdate() {
        for (int i = 0; i < bodies.Length; i++ ){
            bodies[i].UpdateVelocity();
        }
       
        for (int i = 0; i < bodies.Length; i++){
            if (bodies[i].isVisualizing == false)
                bodies[i].UpdatePosition();
            else bodies[i].UpdateLine();
        }
    
   }
}
