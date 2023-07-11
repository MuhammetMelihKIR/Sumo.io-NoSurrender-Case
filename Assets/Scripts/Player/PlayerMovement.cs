using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private float _rotateSpeed;
    private Rigidbody _rb;
    private Vector3 _moveVector;
    public float moveSpeed;
    public bool canMove;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
  

    private void Update()
    {
        PlayerMove();
    }

    public void PlayerMove()
    {
        if (canMove)
        {
            _moveVector = Vector3.zero;
            _moveVector.x = _joystick.Horizontal * moveSpeed * Time.deltaTime;
            _moveVector.z = _joystick.Vertical * moveSpeed * Time.deltaTime;

            if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
            {
                Vector3 direction = Vector3.RotateTowards(transform.forward, _moveVector, _rotateSpeed * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(direction);
            }

            _rb.MovePosition(_rb.position + _moveVector);
        }
        
           
    }


}
