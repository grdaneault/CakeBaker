using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorMaker : MonoBehaviour {


    public Door DoorTemplate;
    public LeverPull DoorLever;
    private List<DoorSlot> _doorSlots;

    private int _doorIndex;
    private bool _createDoors = true;

    // Use this for initialization
    void Start () {
        _doorSlots = GetComponentsInChildren<DoorSlot>().ToList();

    }

    // Update is called once per frame
    void Update () {


        if ((DoorLever != null && DoorLever.JustTriggered) || Input.GetKeyDown(KeyCode.D))
        {
            for (var i = 0; i < _doorSlots.Count; i++)
            {

                var door = _doorSlots[(_doorIndex + i) % _doorSlots.Count];
                if (door.Door == null && door.CanMakeDoor)
                {
                    door.CreateDoor(DoorTemplate);
                    _doorIndex = (_doorIndex + i + Random.Range(1, 3)) % _doorSlots.Count;
                    break;
                }
            }
        }

        //if ()
        //{

        //    var slot = _doorSlots[_doorIndex];
        //    if (_createDoors)
        //    {
        //        slot.CreateDoor(DoorTemplate);

        //    } else
        //    {
        //        slot.HideDoor();
        //    }
        //    _doorIndex += 1;

        //    if (_doorIndex == _doorSlots.Count)
        //    {
        //        _doorIndex = 0;
        //        _createDoors = !_createDoors;
        //    }
        //}
    }
}
