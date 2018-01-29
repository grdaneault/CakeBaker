using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestLogic : MonoBehaviour {


    [Serializable]
    public struct ShotBindings
    {
        public KeyCode Key;
        public GameObject Obj;
    }
    [Serializable]
    public struct DoorBindings
    {
        public KeyCode Key;
        public Door Door;
    }

    public DoorBindings[] Doors;
    public ShotBindings[] Shots;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        foreach(var binding in Doors)
        {
            if (Input.GetKeyDown(binding.Key))
            {
                binding.Door.ToggleDoor();
            }
        }
        foreach(var shot in Shots)
        {
            if (Input.GetKeyDown(shot.Key))
            {
                var other = GameObject.Instantiate(shot.Obj);
                other.SetActive(true);
            }
        }

    }
}
