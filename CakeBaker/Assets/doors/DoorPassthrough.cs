using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPassthroughEventArgs : EventArgs
{
    public Collider Collider;
}

public class DoorPassthrough : MonoBehaviour {


    public event EventHandler<DoorPassthroughEventArgs> OnPassThrough;

    public List<DoorPassthroughEventArgs> RecentPassthroughs;

	// Use this for initialization
	void Start () {
        OnPassThrough = (s, a) => { };
        RecentPassthroughs = new List<DoorPassthroughEventArgs>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void LateUpdate()
    {
        RecentPassthroughs.Clear();
    }

    private void OnTriggerExit(Collider other)
    {
        var args = new DoorPassthroughEventArgs() { Collider = other };
        RecentPassthroughs.Add(args);
        OnPassThrough(this, args);
    }
}
