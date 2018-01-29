using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class MouseThrow : MonoBehaviour {

    private Plane _ground;
    private Vector3 _lastHit;

    public GameObject MouseIndicator;
    public Material HighlightMaterial;
    private CanPickUp _pickingUp;

    // Use this for initialization
    void Start () {
        _ground = new Plane(Vector3.up, 0);

    }

    // Update is called once per frame
    void Update () {

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // plane.Raycast returns the distance from the ray start to the hit point
        var distance = 0f;
        if (_ground.Raycast(ray, out distance))
        {
            // some point of the plane was hit - get its coordinates
            var hitPoint = ray.GetPoint(distance);
            _lastHit = hitPoint;
            MouseIndicator.transform.position = hitPoint;
            // use the hitPoint to aim your cannon
        }


        var hits = Physics.RaycastAll(ray);

        var pickUps = hits.Select(h => new HitAndObject()
            {
                Obj = h.collider.gameObject.GetComponent<CanPickUp>(),
                Info = h
            })
            .Where(h => h.Obj != null)
            .ToList();

        pickUps.ForEach(p => p.Obj.Highlight());


        if (Input.GetMouseButtonDown(0))
        {
            var first = pickUps.FirstOrDefault();

            if (first != null)
            {
                _pickingUp = first.Obj;
                _pickingUp.PickUp(MouseIndicator.transform, first.Info.point);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_pickingUp != null)
            {
                _pickingUp.Drop();
                _pickingUp = null;
            }
        }


	}
    public class HitAndObject
    {
        public CanPickUp Obj;
        public RaycastHit Info;
    }
}
