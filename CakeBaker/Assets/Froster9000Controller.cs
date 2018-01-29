using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Froster9000Controller : MonoBehaviour {

    private GameObject frosterBomb;
    public LeverPull FrostingLever;

    // Use this for initialization
    void Start () {
        frosterBomb = Resources.Load<GameObject>("froster/Frosting Bomb");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.LogWarning("Spawning frosting");
            var instance = Instantiate(frosterBomb, this.transform);

        }
        if (FrostingLever != null && FrostingLever.JustTriggered)
        {
            Debug.LogWarning("Spawning frosting");
            var instance = Instantiate(frosterBomb, this.transform);
        }
    }
}
