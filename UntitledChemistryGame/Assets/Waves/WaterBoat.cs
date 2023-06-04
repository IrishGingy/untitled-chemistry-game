using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBoat : MonoBehaviour
{
    public Transform Motor;
    public float SteerPower = 500f;
    public float Power = 5f;
    public float MaxSpeed = 10f;
    public float Drag = 0.1f;

    protected Rigidbody Rigidbody;
    protected Quaternion StartRotation;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        StartRotation = Motor.localRotation;
    }

    void FixedUpdate()
    {
        // default direction
        var forceDirection = transform.forward;
        var steer = 0;

        // steer direction [1, 0, -1]
        if (Input.GetKey(KeyCode.A))
        {
            steer = 1;
        }
        if (Input.GetKey(KeyCode.D)) {  
            steer = -1;
        }

        var dragCoeff = 1 + Rigidbody.drag;

        // Rotational force
        Rigidbody.AddForceAtPosition(steer * transform.right * dragCoeff * Time.deltaTime * SteerPower, Motor.position);

        // compute vectors
        var forward = Vector3.Scale(new Vector3(1, 0, 1), transform.forward);
        var targetVel = Vector3.zero;

        // forward/backward power
        if (Input.GetKey(KeyCode.W))
        {
            ApplyForceToReachVelocity(Rigidbody, forward * MaxSpeed * dragCoeff * Time.deltaTime, Power);
        }
        if (Input.GetKey(KeyCode.S))
        {
            ApplyForceToReachVelocity(Rigidbody, forward * -MaxSpeed * dragCoeff * Time.deltaTime, Power);
        }
    }

    private void ApplyForceToReachVelocity(Rigidbody rigidbody, Vector3 velocity, float force = 1, ForceMode mode = ForceMode.Force)
    {
        if (force == 0 || velocity.magnitude == 0)
            return;

        velocity = velocity + velocity.normalized * 0.2f * rigidbody.drag;

        //force = 1 => need 1 s to reach velocity (if mass is 1) => force can be max 1 / Time.fixedDeltaTime
        force = Mathf.Clamp(force, -rigidbody.mass / Time.fixedDeltaTime, rigidbody.mass / Time.fixedDeltaTime);

        //dot product is a projection from rhs to lhs with a length of result / lhs.magnitude https://www.youtube.com/watch?v=h0NJK4mEIJU
        if (rigidbody.velocity.magnitude == 0)
        {
            rigidbody.AddForce(velocity * force, mode);
        }
        else
        {
            var velocityProjectedToTarget = (velocity.normalized * Vector3.Dot(velocity, rigidbody.velocity) / velocity.magnitude);
            rigidbody.AddForce((velocity - velocityProjectedToTarget) * force, mode);
        }
    }
}
