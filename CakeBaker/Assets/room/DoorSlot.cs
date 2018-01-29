using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorSlot : MonoBehaviour {

    public bool CanMakeDoor = true;

    private ScaleBounce _scaleBounce;

    public Door Door;

    private float _doorAppearsAt, _scaleShrinksAt, _doorOpensAt;
    private bool _shoulDoorAppear, _shouldScaleShrink, _shouldDoorOpen;

    private float _doorDestroysAt, _doorVanishAt, _scaleGrowsAt;
    private bool _shouldDoorDestroy, _shouldDoorVanish, _shouldScaleGrow;

    private Vector3 _originalScale;

	// Use this for initialization
	void Start () {

        _scaleBounce = gameObject.GetComponent<ScaleBounce>();
        if (_scaleBounce == null)
        {
            _scaleBounce = gameObject.AddComponent<ScaleBounce>();
        }

        _originalScale = transform.localScale;
        _scaleBounce.TargetScale = transform.localScale;
        _scaleBounce.ScaleFriction = new Vector3(.1f, .1f, .1f);
        _scaleBounce.ScaleSpring = new Vector3(.1f, .1f, .1f);
	}
	
	// Update is called once per frame
	void Update () {
		
        
        if (Door != null && _shoulDoorAppear && Time.realtimeSinceStartup > _doorAppearsAt)
        {
            Door.Appear();
            _shoulDoorAppear = false;
        }
        if (Door != null && _shouldDoorOpen && Time.realtimeSinceStartup > _doorOpensAt)
        {
            Door.OpenDoor();
            _shouldDoorOpen = false;
        }
        if (Door != null && _shouldScaleShrink && Time.realtimeSinceStartup > _scaleShrinksAt)
        {
            _scaleBounce.TargetScale = new Vector3(0, 0, 0);
            _scaleBounce.ScaleFriction = new Vector3(.1f, .1f, .1f);

            _scaleBounce.ScaleFriction = new Vector3(.1f, .1f, .1f);

            _shouldScaleShrink = false;
        }
        if (Door != null && _shouldScaleGrow && Time.realtimeSinceStartup > _scaleGrowsAt)
        {
            _scaleBounce.GetComponent<BoxCollider>().enabled = true;

            //_scaleBounce.TargetScale.z *= .1f;
            _shouldScaleGrow = false;
        }

        if (Door != null && _shouldDoorDestroy && Time.realtimeSinceStartup > _doorDestroysAt)
        {
            GameObject.Destroy(Door.gameObject);
            Door = null;
            _shouldDoorDestroy = false;
        }
        if (Door != null && _shouldDoorVanish && Time.realtimeSinceStartup > _doorVanishAt)
        {
            Door.Vanish();
            _shouldDoorVanish = false;
        }
        
    }

    public void CreateDoor(Door doorTemplate)
    {
        if (!CanMakeDoor)
        {
            return;
        }
        //transform.localScale = new Vector3(1, 1, .1f);
        //_scaleBounce.TargetScale = new Vector3(0, 0, 0);

        Door = Instantiate<Door>(doorTemplate);
        Door.transform.parent = transform.parent;
        Door.transform.localPosition = Vector3.zero + Vector3.forward * .05f ;
        Door.transform.localRotation = Quaternion.identity;

        Door.IsOpen = false;
        Door.IsVisible = false;

        Door.OnClosed += (s, a) =>
        {
            _shouldDoorDestroy = true;
            _shouldDoorVanish = true;
            _shouldScaleGrow = true;
            _doorDestroysAt = Time.realtimeSinceStartup + 1f;
            _doorVanishAt = Time.realtimeSinceStartup + .4f;
            _scaleGrowsAt = Time.realtimeSinceStartup + .4f;

            _scaleBounce.GetComponent<BoxCollider>().enabled = false;
            _scaleBounce.ScaleFriction = new Vector3(.2f, .2f, .2f);
            _scaleBounce.ScaleFriction = new Vector3(.5f, .5f, .5f);
            _scaleBounce.TargetScale = _originalScale;
        };

        _doorAppearsAt = Time.realtimeSinceStartup + .1f;
        _scaleShrinksAt = Time.realtimeSinceStartup + .6f;
        _doorOpensAt = Time.realtimeSinceStartup + 1;

        _shouldDoorOpen = true;
        _shoulDoorAppear = true;
        _shouldScaleShrink = true;

        //door.transform.localRotation = transform.parent.rotation;
    }

    public void HideDoor()
    {
        if (Door == null)
        {
            return;
        }

        Door.CloseDoor();

        
    }
}
