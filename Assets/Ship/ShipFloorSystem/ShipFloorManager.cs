using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFloorManager : MonoBehaviour
{
	[SerializeField] public GameObject CurrentShipFloor;
    [SerializeField] List<GameObject> ShipFloors = new List<GameObject>();
	public bool isLowerDeck { get { return CurrentShipFloor.name == "ShipBottomDeck"; } }

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
