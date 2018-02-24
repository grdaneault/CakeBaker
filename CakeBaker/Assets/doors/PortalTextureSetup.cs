using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetup : MonoBehaviour {

    public Camera PortalCamera;
    public Material PortalMaterial;

	// Use this for initialization
	void Start () {
		if (PortalCamera.targetTexture != null)
        {
            PortalCamera.targetTexture.Release();
        }
        PortalCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        PortalMaterial.mainTexture = PortalCamera.targetTexture;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
