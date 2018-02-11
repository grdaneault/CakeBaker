using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Door : MonoBehaviour {

    [Header("Internals")]
    public DoorHinge DoorHinge;
    public SlamEffect SlamEffect;

    [Header("Game")]
    public bool IsOpen;
    public bool IsVisible;
    public bool IsFlipped;
    public int RequiredSlamVelocity = 100;
    public int SlamCompleteAngleThreshold = 5;
    public int OpenDegree = 100;
    public float MaxOpen = 120;

    [Header("Debug")]
    public float _doorVelocity;
    public float _doorAngle;
    public bool _isOpen;
    public bool _isVisible;
    public bool _isFlipped;
    public bool _vanishOnClose;
    public bool _firstClose;
    public float _doorBounce;
    public float _doorLimitMin, _doorLimitMax;

    public event EventHandler OnClosed = (s, a) => { };

    private Vector3 _targetScale = Vector3.one;
    private Vector3 _scaleVelocity = Vector3.zero;
    public Vector3 ScaleSpring = new Vector3(.1f, .2f, .1f);
    public Vector3 ScaleFriction = new Vector3(.01f, .01f, .01f);

	// Use this for initialization
	void Start () {

        if (IsOpen)
        {
            OpenDoor();
        }
        if (!IsVisible)
        {
            VanishInstantly();
        }
        //Hinge.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Vanish();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Appear();
        }

       
        //foreach (var body in gameObject.GetComponentsInChildren<Rigidbody>())
        //{
        //    body.Sleep();
        //}
        ////foreach (var body in gameObject.GetComponentsInChildren<HingeJoint>())
        ////{
        ////    body.
        ////}


        UpdateIsOpen();
        UpdateIsVisible();
        UpdateIsFlipped();
        UpdateScale();

       

        if (_vanishOnClose && DoorHinge.Hinge != null)
        {
            if (DoorHinge.Hinge.angle > -SlamCompleteAngleThreshold)
            {
                _vanishOnClose = false;
                Vanish();
            }
        }

        if (DoorHinge.Hinge != null)
        {

            _doorLimitMax = DoorHinge.Hinge.limits.max;
            _doorLimitMin = DoorHinge.Hinge.limits.min;

            DoorHinge.Flipped = IsFlipped;

            



            var slamAngle = IsFlipped ? -180 + SlamCompleteAngleThreshold : -SlamCompleteAngleThreshold;
            DoorHinge.Hinge.limits = new JointLimits()
            {
                max = IsFlipped ? -180 + MaxOpen : 0,
                min = IsFlipped ? -180 : -MaxOpen,
                bounciness = .4f
            };
            if (_isOpen)
            {
                
            }
            else
            {
                //DoorHinge.Hinge.limits = new JointLimits()
                //{
                //    max = IsFlipped ? -180 : 0,
                //    min = IsFlipped ? -180 : 0,
                //    bounciness = .0f
                //};
            }

            //if (_isOpen)
            //{

            //    if (DoorHinge.Hinge.angle > -SlamCompleteAngleThreshold && DoorHinge.Hinge.velocity > RequiredSlamVelocity)
            //    {
            //        CloseDoor(); // SLAMMED!
            //        SlamEffect.Slam();
            //        Debug.Log("SLAMMED");
            //    }
            //}
            //else
            //{
            //    if (DoorHinge.Hinge.angle > -SlamCompleteAngleThreshold)
            //    {
            //        if (_firstClose)
            //        {
            //            _firstClose = false;
            //            OnClosed(this, null);
            //        }
            //    }
            //    if (DoorHinge.Hinge.angle > -SlamCompleteAngleThreshold && DoorHinge.Hinge.velocity < 0)
            //    {

            //        DoorHinge.Hinge.limits = new JointLimits()
            //        {
            //            min = 0,
            //            bounciness = 0
            //        };
            //    }
            //}
            DoorHinge.Hinge.useSpring = true;
            _doorVelocity = DoorHinge.Hinge.velocity;
            _doorAngle = DoorHinge.Hinge.angle;
        }
    }
    
    public void StopOpenClose()
    {
        DoorHinge.EnsureHinge();

        DoorHinge.Hinge.spring = new JointSpring()
        {
            spring = .05f,
            damper = .001f,
            targetPosition = -OpenDegree
        };
    }

    public void OpenDoor()
    {
        DoorHinge.EnsureHinge();

        _isOpen = true;
        IsOpen = true;
        DoorHinge.Hinge.spring = new JointSpring()
        {
            spring = .2f,
            damper = .02f,
            targetPosition = IsFlipped ? -180 + OpenDegree : -OpenDegree
        };
        
    }

    //private void GetLimits()
    //{

    //}

    public void CloseDoor()
    {

        if (_isOpen)
        {
            _firstClose = true;

        }
        DoorHinge.EnsureHinge();

        _isOpen = false;
        IsOpen = false;
        DoorHinge.Hinge.spring = new JointSpring()
        {
            spring = .4f,
            damper = .02f,
            targetPosition = IsFlipped ? -180 : 0
        };
    }

    public void ToggleDoor()
    {
        if (_isOpen)
        {
            CloseDoor();
        } else
        {
            OpenDoor();
        }
    }

    public void ToggleVisibility()
    {
        if (_isVisible)
        {
            Vanish();
        } else
        {
            Appear();
        }
    }
    public void VanishWhenClosed()
    {
        _vanishOnClose = true;
    }

    public void Vanish()
    {
        //GetComponentsInChildren<Rigidbody>(true).ToList().ForEach(r => DestroyImmediate(r));
        //GetComponentsInChildren<HingeJoint>(true).ToList().ForEach(r => DestroyImmediate(r));
        if (IsOpen)
        {
            CloseDoor();
        }
        DoorHinge.RemoveHinge();
        _targetScale = Vector3.zero;
        _isVisible = false;
        IsVisible = false;
    }

    public void VanishInstantly()
    {
        if (IsOpen)
        {
            CloseDoor();
        }
        DoorHinge.RemoveHinge();
        _targetScale = Vector3.zero;
        transform.localScale = _targetScale;
        _isVisible = false;
        IsVisible = false;
    }

    public void Appear()
    {
        _targetScale = Vector3.one;
        _isVisible = true;
        IsVisible = true;
    }

    public void FlipDoor()
    {
        if (DoorHinge != null)
        {
            //DoorHinge.TheHinge.transform.localRotation = Quaternion.Euler(0, 180, 0);
            //DoorHinge.Flipped = true;
        }
    }

    public void UnflipDoor()
    {
        if (DoorHinge == null)
        {
            //DoorHinge.TheHinge.transform.localRotation = Quaternion.Euler(0, 0, 0);
            //DoorHinge.Flipped = false;
        }
    }

    private void UpdateIsOpen()
    {
        if (IsOpen != _isOpen)
        {
            _isOpen = IsOpen;
            if (_isOpen)
            {
                OpenDoor();
            }
            else
            {
                CloseDoor();
            }
        }
    }

    private void UpdateIsVisible()
    {
        if (IsVisible != _isVisible)
        {
            _isVisible = IsVisible;
            if (_isVisible)
            {
                Appear();
            } else
            {
                Vanish();
            }
        }
    }

    public void UpdateIsFlipped()
    {
        if (_isFlipped != IsFlipped)
        {
            _isFlipped = IsFlipped;
            if (_isFlipped)
            {
                FlipDoor();
            } else
            {
                UnflipDoor();
            }
        }
    }

    private void UpdateScale()
    {
        var scaleDiff = _targetScale - transform.localScale;

        var spring = new Vector3(
            ScaleSpring.x * scaleDiff.x,
            ScaleSpring.y * scaleDiff.y,
            ScaleSpring.z * scaleDiff.z);

        var friction = new Vector3(
            -ScaleFriction.x * _scaleVelocity.x,
            -ScaleFriction.y * _scaleVelocity.y,
            -ScaleFriction.z * _scaleVelocity.z);

        var acc = (friction + spring) / 1.0f; //mass

        _scaleVelocity += acc;
        transform.localScale += _scaleVelocity;

        transform.localScale = new Vector3(
            Mathf.Clamp(transform.localScale.x, 0, 1.5f),
            Mathf.Clamp(transform.localScale.y, 0, 1.5f),
            Mathf.Clamp(transform.localScale.z, 0, 1.5f));

    }
}
