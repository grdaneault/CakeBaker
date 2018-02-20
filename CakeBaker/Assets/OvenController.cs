using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenController : MonoBehaviour {

    
    private GameObject cake;
    public Vector3 cakeForce = new Vector3(0, 100, 500);

    public LeverPull CakeLever;

    // Use this for initialization
    void Start () {
        cake = Resources.Load<GameObject>("cake/Cake");
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.C) || (CakeLever != null && CakeLever.JustTriggered))
        {
            SpawnCake();
        }
    }

    public void SpawnCake()
    {
        Debug.LogWarning("Spawning cake");
        var instance = Instantiate(cake, this.transform);

        var forceDirection = transform.TransformDirection(cakeForce.normalized);
        instance.GetComponentInChildren<Rigidbody>().AddForce(cakeForce.magnitude * forceDirection);
    }
}
