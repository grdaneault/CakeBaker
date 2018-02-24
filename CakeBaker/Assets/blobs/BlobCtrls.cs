using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobCtrls : MonoBehaviour {

    public GameObject BlobObject;

    public float 
        GroundRadius = 1.47f,
        TummyRadius = .4f, 
        HeadRadius = 1.5f, 
        TipRadius = 0;
    public Vector4 
        GroundCtrl = new Vector4(0, -1.07f, 0, 1), 
        TummyCtrl = new Vector4(0, -.84f, 0, 1), 
        HeadCtrl = new Vector4(0, .94f, 0, 1), 
        TipCtrl = new Vector4(0, 1f, 0, 1);

    public bool IsBigGround;

    public const string GroundRadiusShaderKey = "_GroundRadius";
    public const string TummyRadiusShaderKey = "_TummyRadius";
    public const string HeadRadiusShaderKey = "_HeadRadius";
    public const string TipRadiusShaderKey = "_TipRadius";

    public const string GroundCtrlShaderKey = "_GroundCtrl";
    public const string TummyCtrlShaderKey = "_TummyCtrl";
    public const string HeadCtrlShaderKey = "_HeadCtrl";
    public const string TipCtrlShaderKey = "_TipCtrl";

    public Animator _animator;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        var material = BlobObject.GetComponent<MeshRenderer>().material;

        //_animator.SetBool("IsBigGround", IsBigGround);

        material.SetFloat(GroundRadiusShaderKey, GroundRadius);
        material.SetFloat(TummyRadiusShaderKey, TummyRadius);
        material.SetFloat(HeadRadiusShaderKey, HeadRadius);
        material.SetFloat(TipRadiusShaderKey, TipRadius);

        material.SetVector(GroundCtrlShaderKey, GroundCtrl);
        material.SetVector(TummyCtrlShaderKey, TummyCtrl);
        material.SetVector(HeadCtrlShaderKey, HeadCtrl);
        material.SetVector(TipCtrlShaderKey, TipCtrl);

    }
}
