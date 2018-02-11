using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour {

    public SteamVR_Controller.Device Controller { get { return SteamVR_Controller.Input((int)trackedObject.index); } }
    private SteamVR_TrackedObject trackedObject;
    private Joint pickupJoint;
    private InteractionBase colliding;

    // Use this for initialization
    void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        pickupJoint = GetComponent<Joint>();
    }

    // Update is called once per frame
    void Update () {
        if (colliding == null)
        {
            return;
        }

		if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            colliding.OnTriggerDown(this);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        var interactionBase = other.GetComponent<InteractionBase>();
        if (interactionBase == null)
        {
            return;
        }

        if (colliding != null)
        {
            colliding.Release(this);
        }

        interactionBase.Capture(this);
    }
}
