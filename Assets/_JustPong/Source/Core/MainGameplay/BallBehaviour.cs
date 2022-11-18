using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    [Header("Movement Configs")]
    [SerializeField]
    private float _DesiredSpeed;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        InitializeMovement();
    }

    private void FixedUpdate()
    {
        if (_rigidbody2D.velocity.magnitude > _DesiredSpeed)
        {
            _rigidbody2D.velocity = Vector2.ClampMagnitude(_rigidbody2D.velocity, _DesiredSpeed);
        }
    }

    private void InitializeMovement()
    {
        float mass = _rigidbody2D.mass;
        Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        Vector2 acceleration = _DesiredSpeed * direction / Time.fixedDeltaTime;

        _rigidbody2D.AddForce(mass * acceleration);
    }
}
