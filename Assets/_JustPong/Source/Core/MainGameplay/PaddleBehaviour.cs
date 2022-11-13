using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleBehaviour : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;

    [Header("Movement Configs")]
    [SerializeField]
    private float _MovementSpeed = 10f;

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
        private bool _isFixed = false;
        private int _value = 0;
        public int Value
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
                    _isFixed = false;
                }

                if (_isFixed) return _value;

                if (_isOverlapped)
                {
                    if (_value == 1)
                    {
                        _value = -1;
                    }
                    else if (_value == -1)
                    {
                        _value = 1;
                    }
                    _isFixed = true;
                }
                else if (_up)
                {
                    _value = 1;
                }
                else if (_down)
                {
                    _value = -1;
                }
                else
                {
                    _value = 0;
                }

                return _value;
            }
        }
    }
    private readonly Direction _direction = new();

    private Vector2 _halfSize;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();

        _halfSize = _collider2D.bounds.extents;
    }

    private void FixedUpdate()
    {
        MovePaddle();
    }

    private void Update()
    {
        _velocity = _MovementSpeed * _direction.Value * Vector2.up;
    }

    private void ClampPaddle(float bound)
    {
        _rigidbody2D.MovePosition(new Vector2(_rigidbody2D.position.x, bound));
    }

    private void MovePaddle()
    {
        var target = _rigidbody2D.position + _velocity * Time.fixedDeltaTime;

        var lowerBound = CameraHelper.Instance.LowerBound + _halfSize.y;
        var upperBound = CameraHelper.Instance.UpperBound - _halfSize.y;

        if (target.y < lowerBound)
        {
            ClampPaddle(lowerBound);
        }
        else if (target.y > upperBound)
        {
            ClampPaddle(upperBound);
        }
        else
        {
            _rigidbody2D.MovePosition(target);
        }
    }

    public void MoveUp(InputAction.CallbackContext context) => _direction.Up = context.ReadValueAsButton();
    public void MoveDown(InputAction.CallbackContext context) => _direction.Down = context.ReadValueAsButton();
}
