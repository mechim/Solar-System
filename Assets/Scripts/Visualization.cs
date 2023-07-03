using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Visualization : MonoBehaviour
{
    public int iterations;
    public CelestialBody[] allBodies;
    public List<Vector3> ref_positions;
    public List<float> ref_masses;
    public List<LineRenderer> lineRenderers;
    public List<Vector3> ref_velocities;
    public Universe universe;
    private List<Vector3> linePoints;
    private int lineCount;

    private void Start() {
        allBodies = FindObjectsOfType<CelestialBody>();
        universe = FindObjectOfType<Universe>();
        linePoints = new List<Vector3>();
        ref_positions = new List<Vector3>();
        ref_masses = new List<float>();
        ref_velocities = new List<Vector3>();   
        lineRenderers = new List<LineRenderer>();

        foreach (var body in allBodies){
            ref_positions.Add(body.transform.position);
            ref_masses.Add(body.rb.mass);
            lineRenderers.Add(body.transform.GetComponent<LineRenderer>());
        }
    }

    private void Update(){
        for (int i = 0; i < iterations; i++){
            UpdateRefVelocity();
        }
        for (int i = 0; i < iterations; i++){
            UpdateLine();
        }
    }

    private void UpdateRefVelocity(){
        for (int i = 0; i < ref_positions.Count; i ++){
            for (int j = 0; j < ref_positions.Count; j ++){
                if (i != j){
                    // this - i ; other - j
                    var temp_distance = Vector3.Distance(ref_positions[i], ref_positions[j]);
                    var temp_direction = (ref_positions[j] - ref_positions[i]).normalized;
                    var temp_force = temp_direction * universe.gravitationalConstant*(ref_masses[i] * ref_masses[j])/temp_distance*temp_distance; 
                    var temp_acc = temp_force/ref_masses[i];
                    ref_velocities[i] += temp_acc;
                }
            }
        }
    }

    public void UpdateLine(){
        lineCount++;
        for ( int i = 0; i < ref_positions.Count-1; i ++){
            ref_positions[i] += ref_velocities[i];
            // linePoints.Add(ref_positions[i]);
            lineRenderers[i].positionCount = lineCount;
            lineRenderers[i].SetPosition(lineCount -1, ref_positions[i]);
        }
    }


}
