using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CollectionZones : MonoBehaviour {
    public Canvas canvas;
    public Text text;
    public Transform cameraTransform;

	void Start () {
	
	}
	
    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.layer == LayerMask.NameToLayer("Region"))
        {
            canvas.enabled = true;
            canvas.transform.localPosition = trigger.gameObject.transform.localPosition;
            text.text = "Press e to enter the " + trigger.name;
        }
    }

    void OnTriggerExit(Collider other)
    {
        canvas.enabled = false;
    }

    void Update()
    {
        text.transform.LookAt(transform.position + cameraTransform.rotation * Vector3.back, cameraTransform.rotation * Vector3.up);
    }
}
