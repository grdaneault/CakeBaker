using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionBase : MonoBehaviour {

    private MeshRenderer _renderer { get { return GetComponent<MeshRenderer>(); } }

    private Material[] _materials;
    private Material[] _materialsWithHighlight;

    public Material HighlightMaterial;

    private List<HandController> controllingHands = new List<HandController>(2);

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

    public void HoverEnter(HandController hand)
    {
        if (!controllingHands.Contains(hand))
        {
            if (controllingHands.Count == 0)
            {
                Highlight();
            }
            controllingHands.Add(hand);
        }
        else
        {
            Debug.LogError("Already controlled by this hand!");
        }
    }

    public void HoverExit(HandController hand)
    {
        if (controllingHands.Contains(hand))
        {
            controllingHands.Remove(hand);
            if (controllingHands.Count == 0)
            {
                UnHighlight();
            }
        }
        else
        {
            Debug.LogError("Did not control this hand!");
        }
    }

    private void Highlight()
    {
        if (_renderer != null)
        {
            UpdateMaterials();
            _renderer.materials = _materialsWithHighlight;
        }
    }

    private void UnHighlight()
    {
        if (_renderer != null)
        {
            UpdateMaterials();
            _renderer.materials = _materials;
        }
    }

    public void OnTriggerDown(HandController hand)
    {
        foreach (var ch in controllingHands)
        {
            if (ch != hand && ch.IsHolding(this))
            {
                ch.ThrowObject(this);
            }
            else if (ch == hand && !hand.IsHolding(this))
            {
                hand.PickupObject(this);
            }
        }
    }

    public void OnTriggerUp(HandController hand)
    {
        if (controllingHands.Contains(hand))
        {
            // Only throw if we are currently held by this hand 
            // (trigger on one cake, steal with other hand, move to new cake, release trigger -> can't throw)
            if (hand.IsHolding(this))
            {
                hand.ThrowObject(this);
            }
        }
        else
        {
            Debug.LogError("Trigger up on not-owned hand!");
        }
    }
}
