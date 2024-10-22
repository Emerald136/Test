using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    public Camera playerCamera;
    public float pickUpRange = 3.0f;
    public float moveForce = 250f;
    private GameObject heldObject = null;
    private Rigidbody heldObjectRb;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (heldObject == null)
            {
                TryPickUpObject();
            }
            else
            {
                DropObject();
            }
        }

        if (heldObject != null)
        {
            MoveObject();
        }
    }

    void TryPickUpObject()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            if (hit.collider.GetComponent<Rigidbody>())
            {
                heldObjectRb = hit.collider.GetComponent<Rigidbody>();
                heldObjectRb.useGravity = false;
                heldObjectRb.drag = 10;
                heldObjectRb.constraints = RigidbodyConstraints.FreezeRotation;
                heldObject = hit.collider.gameObject;
            }
        }
    }

    void MoveObject()
    {
        Vector3 targetPosition = playerCamera.transform.position + playerCamera.transform.forward * 2f;
        Vector3 moveDirection = targetPosition - heldObject.transform.position;

        heldObjectRb.AddForce(moveDirection * moveForce);

        if (Vector3.Distance(heldObject.transform.position, targetPosition) > 0.1f)
        {
            heldObjectRb.velocity = Vector3.ClampMagnitude(heldObjectRb.velocity, 10);
        }
    }

    void DropObject()
    {
        heldObjectRb.useGravity = true;
        heldObjectRb.drag = 1;
        heldObjectRb.constraints = RigidbodyConstraints.None;
        heldObject = null;
        heldObjectRb = null;
    }
}

