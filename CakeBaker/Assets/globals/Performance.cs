using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Performance : MonoBehaviour {

    public int TargetFramerate = 60;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        QualitySettings.vSyncCount = 0;

        Application.targetFrameRate = TargetFramerate;
	}
}
