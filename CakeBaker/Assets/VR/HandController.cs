using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour {

    public SteamVR_Controller.Device Controller { get { return SteamVR_Controller.Input((int)trackedObject.index); } }
    private SteamVR_TrackedObject trackedObject;
    private Joint pickupJoint;
    private List<InteractionBase> hovering = new List<InteractionBase>();
    private InteractionBase holding;

    // Use this for initialization
    void Start()
    {
        trackedObject = GetComponentInParent<SteamVR_TrackedObject>();
        pickupJoint = GetComponent<Joint>();
    }

    // Update is called once per frame
    void Update () {
        if (hovering.Count == 0 && holding == null)
        {
            return;
        }


        var interactionTarget = holding != null ? holding : GetClosestCollider();

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            interactionTarget.OnTriggerDown(this);
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            interactionTarget.OnTriggerUp(this);
        }
    }

    InteractionBase GetClosestCollider()
    {
        InteractionBase closestCollider = null;
        var toRemove = new List<InteractionBase>();
        foreach (var hit  in hovering)
        {
            if (!hit.isActiveAndEnabled)
            {
                toRemove.Add(hit);
                continue;
            }

            if (closestCollider == null)
            {
                closestCollider = hit;
            }
            //compares distances
            if (Vector3.Distance(transform.position, hit.transform.position) <= Vector3.Distance(transform.position, closestCollider.transform.position))
            {
                closestCollider = hit;
            }
        }

        foreach (var getRidOfMe in toRemove)
        {
            hovering.Remove(getRidOfMe);
        }

        return closestCollider;
    }

    private void OnTriggerEnter(Collider other)
    {
        var interactionBase = other.GetComponent<InteractionBase>();
        if (interactionBase == null) { return; }

        interactionBase.HoverEnter(this);
        hovering.Add(interactionBase);
    }

    private void OnTriggerExit(Collider other)
    {
        var interactionBase = other.GetComponent<InteractionBase>();
        if (interactionBase == null) { return; }

        hovering.Remove(interactionBase);
        interactionBase.HoverExit(this);
    }

    public void PickupObject(InteractionBase obj)
    {
        if (holding != null)
        {
            Debug.LogAssertion("Cannot pull trigger while already holding an object called " + holding.name, obj);
        }
        pickupJoint.connectedBody = obj.GetComponent<Rigidbody>();
        holding = obj;
        Debug.Log("Picked up object", obj);
    }

    public void ThrowObject(InteractionBase obj)
    {
        if (obj != holding)
        {
            Debug.LogAssertion("Cannot throw a non-held object", obj);
            return;
        }
        pickupJoint.connectedBody = null;
        var rb = obj.GetComponent<Rigidbody>();
        var origin = trackedObject.transform.parent;
        rb.velocity = origin.TransformVector(Controller.velocity);
        rb.angularVelocity = origin.TransformVector(Controller.angularVelocity);
        Debug.Log("Threw with velocity " + rb.velocity, obj);
        holding = null;
    }

    public bool IsHolding(InteractionBase obj)
    {
        return obj == holding;
    }
}
