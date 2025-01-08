using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class VRBikeController : MonoBehaviour
{
    public WheelCollider frontWheel;
    public WheelCollider backWheel;
    public Transform frontWheelMesh;
    public Transform backWheelMesh;

    public Transform throne;
    public float maxTorque = 100f;
    public float torqueMultiplier = 10f;

    private Rigidbody bikeRb;

    void Start()
    {
        bikeRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get the throne's rotation
        float throneRotation = throne.localEulerAngles.x; // Adjust axis as needed
        throneRotation = Mathf.Clamp(throneRotation, 0, 90); // Limit rotation
        
        // Apply motor torque based on throne's angle
        float motorTorque = throneRotation / 90f * maxTorque * torqueMultiplier;
        backWheel.motorTorque = motorTorque;

        // Sync visual wheels with colliders
        UpdateWheelPose(frontWheel, frontWheelMesh);
        UpdateWheelPose(backWheel, backWheelMesh);
    }

    private void UpdateWheelPose(WheelCollider collider, Transform mesh)
    {
        Vector3 pos;
        Quaternion quat;
        collider.GetWorldPose(out pos, out quat);

        mesh.position = pos;
        mesh.rotation = quat;
    }
}
