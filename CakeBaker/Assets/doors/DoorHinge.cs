using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHinge : MonoBehaviour {

    public Door Door;

    public HingeJoint Hinge;
    public GameObject TheHinge;

    //private GameObject _hingeOther;

	// Use this for initialization
	void Start () {
        Hinge = GetComponent<HingeJoint>();
        
	}
	
	// Update is called once per frame
	void Update () {
		
        if (Hinge != null)
        {
            Hinge.anchor = new Vector3(-.3f, 0, 0);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var dontStop = collision.gameObject.GetComponent<DontStopDoor>();
        if (dontStop == null)
        {
            Door.StopOpenClose();
        }
    }

    public void RemoveHinge()
    {
        if (Hinge != null)
        {
            //_hingeOther = Hinge.connectedBody.gameObject;
            GameObject.DestroyImmediate(Hinge.connectedBody);
            GameObject.DestroyImmediate(Hinge);
            DestroyImmediate(gameObject.GetComponent<Rigidbody>());
        }
    }
    public void EnsureHinge()
    {
        if (Hinge == null)
        {
            var otherRigidBody = TheHinge.AddComponent<Rigidbody>();
            otherRigidBody.useGravity = false;
            otherRigidBody.constraints = RigidbodyConstraints.FreezeAll;

            Hinge = gameObject.AddComponent<HingeJoint>();
            Hinge.anchor = new Vector3(-.3f, 0, 0);
            Hinge.axis = new Vector3(0, 1, 0);
            Hinge.autoConfigureConnectedAnchor = false;
            Hinge.connectedAnchor = Vector3.zero;
            Hinge.useLimits = true;
            
            var rigidBody = gameObject.GetComponent<Rigidbody>();
            rigidBody.useGravity = false;
            rigidBody.mass = .2f;

            Hinge.connectedBody = otherRigidBody;
        }
    }
}
