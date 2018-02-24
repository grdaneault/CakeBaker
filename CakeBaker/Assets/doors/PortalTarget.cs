using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTarget : MonoBehaviour {

    public Camera PortalCamera;
    public Material PortalMaterial;
    public MeshRenderer RenderPlane;
    public Shader CutoutShader;

    public Transform PlayerCamera;
    public Transform Portal;
    public Transform OtherPortal;

    private Material cloned;

    // Use this for initialization
    void Start()
    {
        if (PortalCamera.targetTexture != null)
        {
            PortalCamera.targetTexture.Release();
        }
        PortalCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);

        cloned = new Material(PortalMaterial);
        
        cloned.shader = CutoutShader;
        cloned.mainTexture = PortalCamera.targetTexture;

        RenderPlane.material = cloned;
    }

    // Update is called once per frame
    void Update()
    {
        var offset = PlayerCamera.position - OtherPortal.position;

        //transform.localPosition = offset;

        PortalCamera.transform.localPosition = OtherPortal.transform.InverseTransformPoint(PlayerCamera.position);

        float angularDiff = Quaternion.Angle(Portal.rotation, OtherPortal.rotation);
        var rotationalDiff = Quaternion.AngleAxis(angularDiff, Vector3.up);

        var newCamDir = rotationalDiff * PlayerCamera.forward;
        PortalCamera.transform.rotation = Quaternion.LookRotation(newCamDir, Vector3.up);
    }
}
