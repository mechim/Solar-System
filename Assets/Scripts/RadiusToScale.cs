using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RadiusToScale : MonoBehaviour
{
    private CelestialBody celestialBody;


    private void Awake() {
        celestialBody = GetComponent<CelestialBody>();

    }
    private void Update() {
        transform.localScale = Vector3.one * celestialBody.radius;
        celestialBody.mass = celestialBody.surfaceGravity * celestialBody.radius * celestialBody.radius /( Universe.gravitationalConstant * 10000);
            // GetComponent<Rigidbody>().mass = celestialBody.mass;
    }
}
