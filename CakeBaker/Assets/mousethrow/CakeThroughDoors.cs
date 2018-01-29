using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CakeThroughDoors : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        var slots = GetComponentsInChildren<DoorSlot>().ToList();
        foreach (var slot in slots)
        {
            if (slot.Door != null && slot.Door.IsOpen)
            {

                var passthrough = slot.Door.GetComponentInChildren<DoorPassthrough>();
                var events = passthrough.RecentPassthroughs;
                foreach (var e in events)
                {
                    var points = e.Collider.gameObject.GetComponent<CakePoints>();
                    if (points != null)
                    {
                        slot.Door.CloseDoor();
                        //slot.Door.VanishWhenClosed();
                    }
                }
            }
        }

    }
}
