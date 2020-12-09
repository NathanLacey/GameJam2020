using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FuelPickUp : MonoBehaviour
{
    [SerializeField] private float grabDistance = 1.0f;
    [SerializeField] private float throwForce = 1.0f;
    private GameObject currentPickedUpFuel;
    void Awake()
    {
    }

    void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (!currentPickedUpFuel)
            {
                Vector2 grabVector = transform.position + transform.right * grabDistance;
                RaycastHit2D raycastHit2D = Physics2D.Linecast(transform.position, grabVector, (1<<8));
                Debug.DrawLine(transform.position, grabVector);
                if (raycastHit2D)
                {
                    PickUpFuel(raycastHit2D.collider.gameObject);
                }
            }
            else
            {
                DropFuel();
            }
        }
    }

    void PickUpFuel(GameObject fuel)
    {
        currentPickedUpFuel = fuel;
        currentPickedUpFuel.transform.SetParent(transform);
        currentPickedUpFuel.GetComponent<Rigidbody2D>().isKinematic = true;
    }

    void DropFuel()
    {
        currentPickedUpFuel.GetComponent<Rigidbody2D>().isKinematic = false;
        currentPickedUpFuel.GetComponent<Rigidbody2D>().AddForce(transform.right * throwForce);
        currentPickedUpFuel.transform.parent = null;
        currentPickedUpFuel = null;
    }
}
