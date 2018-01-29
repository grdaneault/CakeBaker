using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamEffect : MonoBehaviour {

    public LineRenderer LineTemplate;

    public float EffectRadius = 1.0f;
    public float Height = 1.0f;

    private float _toDeg;


	// Use this for initialization
	void Start () {
        _toDeg = 180f / Mathf.PI;
	}
	
	// Update is called once per frame
	void Update () {
		//if (Input.GetKeyDown(KeyCode.S))
  //      {
  //          Slam();
  //      }
        
	}

    public void Slam()
    {

        var count = 3 + Random.Range(0, 3);
        for (var i = 0; i < count; i++)
        {

            var line = GameObject.Instantiate<LineRenderer>(LineTemplate);
            var exitScript = line.gameObject.AddComponent<SlamLineExit>();
            exitScript.Line = line;
            line.transform.parent = transform;

            var randomPos = EffectRadius * Random.insideUnitCircle;
            randomPos.y = Mathf.Abs(randomPos.y);

            var mag = randomPos.magnitude;
            if (mag < .5)
            {
                randomPos += randomPos.normalized * (.5f - mag);
            }

            line.transform.localPosition = new Vector3(randomPos.x, randomPos.y + Height, 0);

            var angle = _toDeg * Mathf.Atan2(randomPos.y, randomPos.x);
            line.transform.Rotate(new Vector3(-angle, 0, 0));

            var scale = Random.Range(.5f, 1f);
            line.transform.localScale = new Vector3(line.transform.localScale.x, line.transform.localScale.y, line.transform.localScale.z * scale);

        }

    }
}
