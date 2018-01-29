using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CanPickUp : MonoBehaviour {

    private bool _isHovering;
    public float _killHoveringAt;

    private Rigidbody _rigid { get { return GetComponent<Rigidbody>(); } }
    private MeshRenderer _renderer { get { return GetComponent<MeshRenderer>(); } }
    
    private bool _isPicking;
    private Transform _tracking;
    private Vector3 _pullPoint;

    private Material[] _materials;
    private Material[] _materialsWithHighlight;

    public Material HighlightMaterial;

	// Use this for initialization
	void Start () {
        //_rigid = GetComponent<Rigidbody>();

        //_renderer = GetComponent<MeshRenderer>();

        if (_renderer != null)
        {
            _materials = _renderer.materials;

            var set = _materials.ToList();
            set.Add(HighlightMaterial);
            _materialsWithHighlight = set.ToArray();
        }
	}
	
	// Update is called once per frame
	void Update () {


        if (_isHovering == true && Time.realtimeSinceStartup > _killHoveringAt)
        {
            if (_renderer != null)
            {
                _renderer.materials = _materials;

            }

            _isHovering = false;
        }

        if (_isPicking)
        {
            _killHoveringAt = Time.realtimeSinceStartup + .1f;

            var hoverAmount = .2f * (1 - transform.position.y);

            var trackingXY = new Vector2(_tracking.position.x, _tracking.position.z);
            var objectXY = new Vector2(transform.position.x, transform.position.z);
            var trackingDiff = .7f * (trackingXY - objectXY);

            _rigid.AddForceAtPosition(new Vector3(trackingDiff.x, 0, trackingDiff.y), _pullPoint, ForceMode.Impulse);
            _rigid.AddForce(new Vector3(_rigid.velocity.x * -.1f, 0, _rigid.velocity.z * -.1f), ForceMode.Impulse);
            _rigid.AddForceAtPosition(new Vector3(0, hoverAmount, 0), _pullPoint, ForceMode.Impulse);
            _rigid.AddForce(new Vector3(0, _rigid.velocity.y * -.02f, 0), ForceMode.Impulse);

            

        }

	}

    public void PickUp(Transform toTrack, Vector3 pullPoint)
    {
        _isPicking = true;
        _tracking = toTrack;
        _pullPoint = pullPoint;
        _rigid.AddForce(new Vector3(0, 5, 0), ForceMode.Acceleration);
    }

    public void Drop()
    {
        _isPicking = false;
    }

    public void Highlight()
    {

        if (_isHovering == false && _materials != null)
        {
            _renderer.materials = _materialsWithHighlight;

        }

        _isHovering = true;
        _killHoveringAt = Time.realtimeSinceStartup + .1f;
    }
}
