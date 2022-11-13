using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;

    [Header("Movement Configs")]
    [SerializeField]
    private float _MovementSpeed = 7.5f;

    private float _halfWidth;
    private float _halfHeight;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();

        _halfWidth = _collider2D.bounds.extents.x;
        _halfHeight = _collider2D.bounds.extents.y;

        InitializeVelocity();
    }

    private void FixedUpdate()
    {
        ReflectAtBounds();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ReflectOnCollision(other);
    }

    private void InitializeVelocity()
    {
        Vector2 initialVelocity = _MovementSpeed * new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        _rigidbody2D.velocity = initialVelocity;
    }

    private void Reflect(Vector2 normal)
    {
        var newVelocity = _MovementSpeed * Vector2.Reflect(_rigidbody2D.velocity.normalized, normal);
        _rigidbody2D.velocity = newVelocity;
    }

    private void ReflectAtBounds()
    {
        float ballLowerBound = _rigidbody2D.position.y - _halfHeight;
        float ballUpperBound = _rigidbody2D.position.y + _halfHeight;
        float ballLeftBound = _rigidbody2D.position.x - _halfWidth;
        float ballRightBound = _rigidbody2D.position.x + _halfWidth;

        if (ballLowerBound <= CameraHelper.Instance.LowerBound)
        {
            Reflect(Vector2.up);
        }

        if (ballUpperBound >= CameraHelper.Instance.UpperBound)
        {
            Reflect(Vector2.down);
        }

        if (ballLeftBound <= CameraHelper.Instance.LeftBound)
        {
            Reflect(Vector2.right);
        }

        if (ballRightBound >= CameraHelper.Instance.RightBound)
        {
            Reflect(Vector2.left);
        }
    }

    private void ReflectOnCollision(Collision2D other)
    {
        Reflect(other.GetContact(0).normal);
    }
}
