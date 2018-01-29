using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamLineExit : MonoBehaviour {

    public LineRenderer Line;

    public float TimeLeft = .2f;
    public float TimeDecay = .1f;

   
    private float _startTime;

    private Color _startColor;
    private Color _endColor;

	// Use this for initialization
	void Start () {
        _startTime = TimeLeft;
        _startColor = Line.startColor;
        _endColor = Line.endColor;
	}
	
	// Update is called once per frame
	void Update () {

        var newStartColor = _startColor;
        newStartColor.a = GetTime() + .5f;
        var newEndColor = _endColor;
        newEndColor.a = GetTime() + .5f;

        Line.startColor = newStartColor;
        Line.endColor = newEndColor;

        TimeLeft -= Time.deltaTime;

        if (TimeLeft <= 0)
        {
            Destroy(gameObject);
        }
	}

    private float GetTime()
    {
        return TimeLeft / _startTime;
    }

}
