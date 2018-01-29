using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPull : MonoBehaviour {

    public HingeJoint Hinge;

    public int TriggerAngle = 70;
    public int ResetAngle = -70;
    public bool JustTriggered = false;
    public bool Armed = true;

    [Header("debug")]
    public float _angle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Hinge.useSpring = true;
        _angle = Hinge.angle;
        if (JustTriggered)
        {
            JustTriggered = false;
        }
        if (Hinge.angle > 70 && Armed)
        {
            JustTriggered = true;
            Armed = false;
        } 
        if (Hinge.angle < ResetAngle && !Armed)
        {
            Armed = true;
        }
        
	}
}
