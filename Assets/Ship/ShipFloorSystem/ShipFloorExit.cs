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
		if(collision.tag == "Player")
		{
			FloorManager.ChangeToShipFloor(ShipFloorToExitTo);
		}
	}
}
