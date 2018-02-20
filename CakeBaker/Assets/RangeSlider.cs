using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RangeSlider : MonoBehaviour {
    public Transform start;
    public Transform end;

    public UnityEvent ButtonPushed;
    public UnityEvent ButtonReleased;
    private PushState state = PushState.BETWEEN;
    private PushState previousState = PushState.BETWEEN;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = ClosestPointOnLine(transform.position);

        if (transform.position == start.position)
        {
            state = PushState.RELEASED;
        }
        else if (transform.position == end.position)
        {
            state = PushState.PUSHED;
        } else
        {
            state = PushState.BETWEEN;
        }

        if (state == PushState.PUSHED && previousState == PushState.BETWEEN)
        {
            Debug.LogWarning("Pushed!");
            ButtonPushed.Invoke();
        }
        else if (state == PushState.RELEASED && previousState == PushState.BETWEEN)
        {
            Debug.LogWarning("Released!");
            ButtonReleased.Invoke();
        }

        previousState = state;
	}

    Vector3 ClosestPointOnLine(Vector3 point)
    {
        var vectorStart = start.position;
        var vectorEnd = end.position;

        var vVector1 = point - vectorStart;
        var vVector2 = (vectorEnd - vectorStart).normalized;

        float t = Vector3.Dot(vVector2, vVector1);

        if (t <= 0)
            return vectorStart;

        if (t >= Vector3.Distance(vectorStart, vectorEnd))
            return vectorEnd;

        Vector3 vVector3 = vVector2 * t;

        Vector3 vClosestPoint = vectorStart + vVector3;

        return vClosestPoint;
    }

    private enum PushState
    {
        BETWEEN,
        PUSHED,
        RELEASED
    }
}
