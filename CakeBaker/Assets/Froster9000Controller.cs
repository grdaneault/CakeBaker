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
        if (Input.GetKeyDown(KeyCode.F) || (FrostingLever != null && FrostingLever.JustTriggered))
        {
            SpawnFrostingBomb();
        }
    }

    public void SpawnFrostingBomb()
    {
        Debug.LogWarning("Spawning frosting");
        var instance = Instantiate(frosterBomb, this.transform);
    }
}
