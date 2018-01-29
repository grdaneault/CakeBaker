using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBounce : MonoBehaviour {

    public Vector3 TargetScale = Vector3.one;
    public Vector3 ScaleVelocity = Vector3.zero;
    public Vector3 ScaleSpring = new Vector3(.1f, .2f, .1f);
    public Vector3 ScaleFriction = new Vector3(.01f, .01f, .01f);

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var scaleDiff = TargetScale - transform.localScale;

        var spring = new Vector3(
            ScaleSpring.x * scaleDiff.x,
            ScaleSpring.y * scaleDiff.y,
            ScaleSpring.z * scaleDiff.z);

        var friction = new Vector3(
            -ScaleFriction.x * ScaleVelocity.x,
            -ScaleFriction.y * ScaleVelocity.y,
            -ScaleFriction.z * ScaleVelocity.z);

        var acc = (friction + spring) / 1.0f; //mass

        ScaleVelocity += acc;
        transform.localScale += ScaleVelocity;

        transform.localScale = new Vector3(
            Mathf.Clamp(transform.localScale.x, 0, 1.5f),
            Mathf.Clamp(transform.localScale.y, 0, 1.5f),
            Mathf.Clamp(transform.localScale.z, 0, 1.5f));

    }
}
