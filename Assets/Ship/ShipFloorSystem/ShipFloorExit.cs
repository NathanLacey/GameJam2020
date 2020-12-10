using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFloorExit : MonoBehaviour
{
	[SerializeField] GameObject ShipFloorToExitTo;
	[SerializeField] Transform PositionToTeleportPlayerTo;
	ShipFloorManager FloorManager;

	private void Start()
	{
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
				EdgeCollider2D edge = GetComponent<EdgeCollider2D>();
				collision.gameObject.transform.position = new Vector3(edge.bounds.center.x, edge.bounds.center.y, collision.gameObject.transform.position.z);
			}
		}
	}
}
