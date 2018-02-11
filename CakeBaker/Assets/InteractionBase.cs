using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionBase : MonoBehaviour {

    private MeshRenderer _renderer { get { return GetComponent<MeshRenderer>(); } }

    private Material[] _materials;
    private Material[] _materialsWithHighlight;

    public Material HighlightMaterial;

    private List<HandController> controllingHands;

    // Use this for initialization
    void Start () {
        UpdateMaterials();
    }

    void UpdateMaterials()
    {
        if (_renderer != null && _materials == null)
        {
            _materials = _renderer.materials;

            var set = _materials.ToList();
            set.Add(HighlightMaterial);
            _materialsWithHighlight = set.ToArray();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Capture(HandController hand)
    {
        if (!controllingHands.Contains(hand))
        {
            controllingHands.Add(hand);
            Highlight();
        }
        else
        {
            Debug.LogError("Already control this hand!");
        }
    }

    public void Release(HandController hand)
    {
        if (controllingHands.Contains(hand))
        {
            controllingHands.Remove(hand);
            UnHighlight();
        }
        else
        {
            Debug.LogError("Did not control this hand!");
        }
    }

    public void Highlight()
    {
        if (_renderer != null)
        {
            UpdateMaterials();
            _renderer.materials = _materialsWithHighlight;
        }
    }

    public void UnHighlight()
    {
        if (_renderer != null)
        {
            UpdateMaterials();
            _renderer.materials = _materials;
        }
    }

    public void OnTriggerDown(HandController hand)
    {
        
    }
}
