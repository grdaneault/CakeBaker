using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour {

    public Transform PlayerCamera;
    public Transform Portal;
    public Transform OtherPortal;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //var toCam = PlayerCamera.position - OtherPortal.position;

        //var transformedDir = Portal.InverseTransformDirection(toCam.normalized);

        //transform.localPosition = toCam;
        
        var offset = PlayerCamera.position - OtherPortal.position;

        //transform.localPosition = offset;

        transform.localPosition = OtherPortal.transform.InverseTransformPoint(PlayerCamera.position);

        float angularDiff = Quaternion.Angle(Portal.rotation, OtherPortal.rotation);
        var rotationalDiff = Quaternion.AngleAxis(angularDiff, Vector3.up);

        var newCamDir = rotationalDiff * PlayerCamera.forward;
        transform.rotation = Quaternion.LookRotation(newCamDir, Vector3.up);

    }
}
