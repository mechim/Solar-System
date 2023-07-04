using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalVelocityCalc : MonoBehaviour
{
    [SerializeField]
    private CelestialBody body1, body2;
    private float mass;
    private Universe universe;

    private void Start() {
        mass = body1.rb.mass;
        universe = FindObjectOfType<Universe>();
        CalculateOrbitalVeloctiy();
    }
      private void CalculateOrbitalVeloctiy(){
       var distance = Vector3.Distance(body1.transform.position, body2.transform.position);
       var grav = universe.gravitationalConstant;
        var velocity = Mathf.Sqrt((grav*mass)/distance);
        Debug.Log(velocity);
    }
}
