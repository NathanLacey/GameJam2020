using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearMiniGame : MonoBehaviour, IMiniGame
{
    List<IsTriggered> TriggerComponents = new List<IsTriggered>();
    List<DragAndSpin> GearComponents = new List<DragAndSpin>();
    public bool IsFinished { 
        get
		{
            return TriggerComponents.TrueForAll(trigger => trigger.IsBeingTriggered);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        TriggerComponents.AddRange(GetComponentsInChildren<IsTriggered>());
        GearComponents.AddRange(GetComponentsInChildren<DragAndSpin>());
    }

    // Update is called once per frame
    void Update()
    {
        if(IsFinished)
		{
            StopGame();
            Debug.Log("Gear Game Complete!");
		}
    }

    void StopGame()
	{
        GearComponents.ForEach(gear => gear.enabled = false);
	}
}
