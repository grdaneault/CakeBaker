using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour {

    public SteamVR_Controller.Device Controller { get { return SteamVR_Controller.Input((int)trackedObject.index); } }
    private SteamVR_TrackedObject trackedObject;
    private Joint pickupJoint;
    private InteractionBase hovering;
    private InteractionBase holding;

    // Use this for initialization
    void Start()
    {
        trackedObject = GetComponentInParent<SteamVR_TrackedObject>();
        pickupJoint = GetComponent<Joint>();
    }

    // Update is called once per frame
    void Update () {
        if (hovering == null)
        {
            return;
        }

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            hovering.OnTriggerDown(this);
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            hovering.OnTriggerUp(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var interactionBase = other.GetComponent<InteractionBase>();
        if (interactionBase == null)
        {
            return;
        }

        if (hovering != null)
        {
            hovering.HoverExit(this);
        }

        interactionBase.HoverEnter(this);
        hovering = interactionBase;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<InteractionBase>() == hovering && hovering != null)
        {
            hovering.HoverExit(this);
            hovering = null;
        } 
    }

    public void PickupObject(InteractionBase obj)
    {
        pickupJoint.connectedBody = obj.GetComponent<Rigidbody>();
        holding = obj;
        Debug.Log("Picked up object", obj);
    }

    public void ThrowObject(InteractionBase obj)
    {
        if (obj != holding)
        {
            Debug.LogAssertion("Cannot throw a non-held object");
            return;
        }
        pickupJoint.connectedBody = null;
        var rb = obj.GetComponent<Rigidbody>();
        var origin = trackedObject.transform.parent;
        rb.velocity = origin.TransformVector(Controller.velocity);
        rb.angularVelocity = origin.TransformVector(Controller.angularVelocity);
        Debug.Log("Threw with velocity " + rb.velocity, obj);
    }
}
