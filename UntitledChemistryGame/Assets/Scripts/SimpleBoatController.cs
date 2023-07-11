using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleBoatController : MonoBehaviour
{
    [SerializeField] public float maxMoveSpeed;
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private float _speed;
    [SerializeField] private float _velocity;
    [SerializeField] private Vector3 _vectorVelocity;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A)) { _rotation = Vector3.down; }
        else if (Input.GetKey(KeyCode.D)) { _rotation = Vector3.up; }
        else { _rotation = Vector3.zero; }

        transform.Rotate(_rotation * _speed * Time.deltaTime);

        _velocity = _rb.velocity.magnitude;
        _vectorVelocity = _rb.velocity;
    }

    private void FixedUpdate()
    {
        float _moveSpeed = 0;
        if (Input.GetKey(KeyCode.W)) { _moveSpeed = maxMoveSpeed; }
        if (Input.GetKey(KeyCode.S)) { _moveSpeed = -maxMoveSpeed; }

        _rb.AddForce(_moveSpeed * transform.forward, ForceMode.Acceleration);

        _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, maxMoveSpeed);
    }
}
