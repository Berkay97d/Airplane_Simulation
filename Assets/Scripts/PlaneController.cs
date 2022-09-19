using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlaneController : MonoBehaviour
{
    
    #region Speed Values
    
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalSpeed;
    
    #endregion
    
    #region Object References
    
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform propeller;
    [SerializeField] private FloatingJoystick joystick;
    
    #endregion

    #region DoTween References
    
    private Tween propellerTween;
    private Tween airplaneTween;
    
    #endregion


    private void Start()
    {
        RotatePropeller();
    }

    
    private void FixedUpdate()
    {
        ControlAirplane();
        ChangeAirplaneRotation();
    }
    
    
    private void ChangeAirplaneRotation()
    {
        var horizontalTargetRotation = joystick.Horizontal * -30;
        var verticalTargetRotation = joystick.Vertical * -30;

        var targetRotation = Math.Abs(horizontalTargetRotation) >= Math.Abs(verticalTargetRotation) ? new Vector3(0, 0, horizontalTargetRotation) :
                                                                                                            new Vector3(verticalTargetRotation, 0, horizontalTargetRotation/1.2f);
        airplaneTween?.Kill();
        airplaneTween = rb.DORotate(targetRotation, 1f);
    }
    
    
    private void ControlAirplane()
    {
        var horizontal = joystick.Horizontal * horizontalSpeed;
        var vertical = joystick.Vertical * verticalSpeed;

        var target = new Vector3(
                                horizontal,
                                vertical,
                                forwardSpeed
                                );

        
        rb.velocity = target;
    }

    
    private void RotatePropeller()
    {
        propellerTween = propeller.DORotate(new Vector3(0.0f, 0.0f, 360.0f), 0.5f, RotateMode.FastBeyond360);
        propellerTween
            .SetLoops(-1, LoopType.Restart);
    }
    
    
    
}
