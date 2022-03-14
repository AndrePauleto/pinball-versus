using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private bool _isKeyPress = false;
    public float speed = 0f;
    private HingeJoint2D _hingeJoint2D;
    private JointMotor2D _jointMotor;

    void Start()
    {
        _hingeJoint2D = GetComponent<HingeJoint2D>();
        _jointMotor = _hingeJoint2D.motor;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _isKeyPress = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _isKeyPress = false;
        }
    }

    void FixedUpdate()
    {
        if (_isKeyPress == true)
        {
            _jointMotor.motorSpeed = speed;
            _hingeJoint2D.motor = _jointMotor;
        }
        else
        {
            _jointMotor.motorSpeed = -speed;
            _hingeJoint2D.motor = _jointMotor;
        }
    }
}
