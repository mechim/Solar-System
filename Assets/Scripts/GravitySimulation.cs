using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySimulation : MonoBehaviour
{
   CelestialBody[] bodies;
   public bool isVisualizing;

   private void Awake() {
        bodies = FindObjectsOfType<CelestialBody>();
   }

    private void FixedUpdate() {
        if (isVisualizing){
            for (int i = 0; i < bodies.Length; i++ ){
                bodies[i].UpdateRefVelocity();
            }
       
            for (int i = 0; i < bodies.Length; i++){
                bodies[i].UpdateLine();
            }
        } else{
            for (int i = 0; i < bodies.Length; i++ ){
                bodies[i].UpdateVelocity();
            }
        
            for (int i = 0; i < bodies.Length; i++){
                bodies[i].UpdatePosition();
            }
        }
       
    
   }
}
