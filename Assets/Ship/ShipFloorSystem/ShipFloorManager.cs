using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFloorManager : MonoBehaviour
{
	[SerializeField] GameObject CurrentShipFloor;
    [SerializeField] List<GameObject> ShipFloors = new List<GameObject>();

    void Start()
	{
        foreach(GameObject shipFloor in ShipFloors)
		{
			if(shipFloor != CurrentShipFloor)
			{
				shipFloor.SetActive(false);
			}
		}
		CurrentShipFloor.SetActive(true);
	}

	public void ChangeToShipFloor(GameObject shipFloorToChangeTo)
	{
		if(CurrentShipFloor == shipFloorToChangeTo)
		{
			return;
		}
		
		CurrentShipFloor.SetActive(false);
		CurrentShipFloor = shipFloorToChangeTo;
		shipFloorToChangeTo.SetActive(true);
	}
}
