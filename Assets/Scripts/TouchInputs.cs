using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchInputs : MonoBehaviour
{
    bool dragging = false;
    Collider touchPlane;
    GameObject dragAnchor;
    SpringJoint dragSpring;
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        touchPlane = GetComponentInChildren<Collider>();
        touchPlane.enabled = false;
        dragAnchor = touchPlane.transform.GetChild(0).gameObject;
        dragSpring = dragAnchor.GetComponent<SpringJoint>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            if (hit.collider.gameObject.tag == "Map")
            {
                Debug.Log("map tile touched");
            }
            else 
            {

            }






            if (!dragging)
            {
                if(Physics.Raycast(ray, out hit))
                {
                    Rigidbody hitRB = hit.collider.GetComponent<Rigidbody>();
                    if(hitRB == null && hit.collider.gameObject.tag == "Map" )
                    {
                        dragSpring.connectedBody = mainCamera.GetComponent<Rigidbody>();
                        touchPlane.enabled = true;
                        dragging = true;
                    }

                }
            }
            else
            {
                Debug.DrawLine(ray.origin, ray.origin + (ray.direction * 30.0f), Color.red);
                if(Physics.Raycast(ray, out hit, 50.0f))
                {
                    dragAnchor.transform.position = hit.point;

                }
            }       
        }
        else if(dragging)
        {
            dragSpring.connectedBody = null;
            touchPlane.enabled = false;
            dragging = false;
        }
    }
}
