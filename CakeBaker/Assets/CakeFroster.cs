using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeFroster : MonoBehaviour
{

    private GameObject decoratedCake;
    void Start()
    {
        decoratedCake = Resources.Load<GameObject>("cake/DecoratedCake2");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("cake"))
        {
            Debug.LogWarning("Decorating time!!");
            var go = other.gameObject;
            var instance = Instantiate(decoratedCake);

            instance.transform.position = go.transform.position;
            instance.transform.rotation = go.transform.rotation;
            instance.transform.localScale = go.transform.lossyScale;
            
            instance.GetComponentInChildren<Rigidbody>().velocity = go.GetComponentInChildren<Rigidbody>().velocity;
            //instance.GetComponentInChildren<Rigidbody>().position = go.GetComponentInChildren<Rigidbody>().position;
            //instance.GetComponentInChildren<Rigidbody>().rotation = go.GetComponentInChildren<Rigidbody>().rotation;

            GameObject.Destroy(go);
            GameObject.Destroy(this.gameObject);
        }
        else if (other.CompareTag("floor"))
        {
            Debug.LogWarning("You dropped the frosting!");
            GameObject.Destroy(this.gameObject);

        }
        else
        {
            Debug.LogWarning("Not cake!");
        }
    }
}
