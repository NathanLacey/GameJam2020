using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFloorExit : MonoBehaviour
{
	GameObject ThisShipFloor;
	[SerializeField] GameObject ShipFloorToExitTo;
	ShipFloorManager FloorManager;

	private void Start()
	{
		ThisShipFloor = transform.parent.gameObject;
		FloorManager = FindObjectOfType<ShipFloorManager>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag("Player"))
		{
			//Check if the direction the player is going should cause a transition
			Movement playerMovement = collision.gameObject.GetComponent<Movement>();
			Vector2 exitForwardVector = transform.right;
			if(Vector2.Dot(exitForwardVector, playerMovement.MoveDirection) > 0)
			{
				FloorManager.ChangeToShipFloor(ShipFloorToExitTo);
			}
		}
	}
}
