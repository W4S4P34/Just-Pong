using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleBehaviour : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    [Header("Movement Configs")]
    [SerializeField]
    private float _PushPower = 25f;

    private Vector2 _velocity;
    private class Direction
    {
        private bool _up = false;
        private bool _down = false;
        public bool Up
        {
            set { _up = value; }
            get { return _up; }
        }
        public bool Down
        {
            set { _down = value; }
            get { return _down; }
        }

        private bool _isOverlapped = false;
        private bool _isSynced = false;
        private Vector2 _value = Vector2.zero;
        public Vector2 Value
        {
            private set { _value = value; }
            get
            {
                if (_up && _down)
                {
                    _isOverlapped = true;
                }
                else
                {
                    _isOverlapped = false;
                    _isSynced = false;
                }

                if (_isSynced) return _value;

                if (_isOverlapped)
                {
                    if (_value == Vector2.up)
                    {
                        _value = Vector2.down;
                    }
                    else if (_value == Vector2.down)
                    {
                        _value = Vector2.up;
                    }
                    _isSynced = true;
                }
                else if (_up)
                {
                    _value = Vector2.up;
                }
                else if (_down)
                {
                    _value = Vector2.down;
                }
                else
                {
                    _value = Vector2.zero;
                }

                return _value;
            }
        }
    }
    private readonly Direction _direction = new();

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MovePaddle();
    }

    private void Update()
    {
        _velocity = _PushPower * _direction.Value;
    }

    private void MovePaddle()
    {
        _rigidbody2D.AddForce(_rigidbody2D.mass * _velocity);
    }

    public void MoveUp(InputAction.CallbackContext context) => _direction.Up = context.ReadValueAsButton();
    public void MoveDown(InputAction.CallbackContext context) => _direction.Down = context.ReadValueAsButton();
}
