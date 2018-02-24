using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobDisplay : MonoBehaviour {

    public GameObject MeshObject;

    public BezierSpline Spline;

    //public int 
    private Mesh _mesh;

	// Use this for initialization
	void Start () {

        _mesh = MeshObject.GetComponent<MeshFilter>().mesh;
        

	}
	
	// Update is called once per frame
	void Update () {

        var verts = _mesh.vertices;
        for (var i = 0; i < _mesh.vertices.Length; i++)
        {
            var v = verts[i];

            var angle = Mathf.Atan2(v.z, v.x);

            var t = v.y + .5f; // scales y from -.5 to .5 domain to 0 to 1 range.

            var pt = Spline.GetPoint(t);

            var radius = (new Vector2(pt.x, pt.z).magnitude);

            v.x = Mathf.Cos(angle) * radius;
            v.z = Mathf.Sin(angle) * radius;
            //v.y = pt.y;
            verts[i] = v;
        }
        _mesh.vertices = verts;


    }
}
