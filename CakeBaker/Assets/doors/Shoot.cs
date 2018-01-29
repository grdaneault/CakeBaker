using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public Vector3 Force;

    Rigidbody _body;

	// Use this for initialization
	void Start () {
        _body = GetComponent<Rigidbody>();
        _body.AddForce(Force, ForceMode.Impulse);
	}
	
	// Update is called once per frame
	void Update () {


	}
}
