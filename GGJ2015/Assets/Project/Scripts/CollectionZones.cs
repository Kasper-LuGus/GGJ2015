using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CollectionZones : MonoBehaviour
{
    public Canvas canvas;
    public Text text;
    public Camera camera;
    private GameObject currentObject;
    private string activeRegion;
    private GameObject activeObject;
    private List<string> woodPrefabs = new List<string>();

    void Start()
    {
        woodPrefabs.Add("Prefabs/Box");
    }

    void OnTriggerStay(Collider trigger)
    {
        if (trigger.gameObject.layer == LayerMask.NameToLayer("Region") )
        {
            Vector3 cameraVector = camera.transform.forward;
            Vector3 colliderVector = trigger.transform.position - camera.transform.position;
            if (Mathf.Acos(Vector3.Dot(cameraVector, colliderVector) / (cameraVector.magnitude * colliderVector.magnitude)) * 180 / Mathf.PI < 45)
            {
                canvas.enabled = true;

                if (trigger.tag.Equals("item"))
                {
                    text.text = "Press e to pick-up the " + trigger.name;
                    canvas.transform.position = trigger.transform.position;
                }
                else
                {
                    text.text = "Press e to visit the " + trigger.name;
                    canvas.transform.position = trigger.transform.position;
                }
                activeRegion = trigger.name;
                activeObject = trigger.gameObject;
            }
            else if (trigger.name.Equals(activeRegion))
            {
                canvas.enabled = false;
                activeRegion = "";
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.name.Equals(activeRegion)) {
            canvas.enabled = false;
            activeRegion = "";
        }
    }

    void Update()
    {
        text.transform.LookAt(transform.position + camera.transform.rotation * Vector3.back, camera.transform.rotation * Vector3.up);
        if (Input.GetKeyDown("e"))
        {
            if (currentObject == null && !activeRegion.Equals(""))
            {
                switch (activeRegion)
                {
                    case "Shed":
                        break;
                    case "Woods":
                        currentObject = (GameObject)Instantiate(Resources.Load(woodPrefabs[Random.Range(0, woodPrefabs.Count - 1)]), transform.position + (transform.forward * 2), Quaternion.identity);
                        break;
                    default:
                        currentObject = activeObject;
                        currentObject.transform.position = transform.position + (transform.forward * 2);
                        break;

                }
                currentObject.rigidbody.isKinematic = true;
                currentObject.transform.parent = transform;
            }
        }
        if (Input.GetKeyDown("r"))
        {
            if (currentObject != null)
            {
                currentObject.rigidbody.isKinematic = false;
                currentObject.transform.parent = null;
                currentObject = null;
            }
            activeObject = null;
        }
    }
}
